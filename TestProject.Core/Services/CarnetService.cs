using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using TestProject.Core.Commands;
using TestProject.Core.Models;
using TestProject.Core.Services.Interfaces;

namespace TestProject.Core.Services
{
	public class CarnetService : ICarnetService
	{
		private Channel channel;
		private CarNetAI.CarNetAI.CarNetAIClient client;

		public CarnetService()
		{
			channel = new Channel("real.by:50051", ChannelCredentials.Insecure);
			client = new CarNetAI.CarNetAI.CarNetAIClient(channel);
		}

		public async Task<TryResult<CarModel>> TryDetectModel(byte[] image)
		{
			CarModel carModel = new CarModel();

			var postResult = await client.DetectModelAsync(new DetectModelRequest
			{
				Image = ByteString.CopyFrom(image),

			}, null);

			if (!postResult.IsSuccess)
			{
				return TryResult.Unsucceed<CarModel>();
			}

			carModel = postResult.ToCarModel();
			carModel.Bytes = image;

			return TryResult.Success(carModel);
		}
	}
}
