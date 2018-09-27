using System;
using System.Threading.Tasks;
using TestProject.Core.Commands;
using TestProject.Core.Models;

namespace TestProject.Core.Services.Interfaces
{
	public interface ICarnetService
	{
		Task<TryResult<CarModel>> TryDetectModel(byte[] image);
	}
}
