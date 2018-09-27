using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{
	public class ResultViewModel : BaseViewModel<CarModel>
	{
		private CarModel carModelDetails;
		private string carModel;
		private string carMake;
		private string carProbability;

		public ResultViewModel()
		{
		}

		public string CarModel
		{
			get => carModel;
			set => SetProperty(ref carModel, value);
		}

		public string CarMake
		{
			get => carMake;
			set => SetProperty(ref carMake, value);
		}

		public string CarProbability
		{
			get => carProbability;
			set => SetProperty(ref carProbability, value);
		}

		public CarModel CarModelDetails
		{
			get => carModelDetails;
			set => SetProperty(ref carModelDetails, value);
		}

		public override void Prepare(CarModel parameter)
		{
			if (parameter == null)
			{ 
				return;
			}

			CarModelDetails = parameter;
			CarModel = $"Model {CarModelDetails.ModelName}";
			CarMake = $"Make {CarModelDetails.MakeName}";
			CarProbability = $"Probability {CarModelDetails.Probability}";
		}
	}
}
