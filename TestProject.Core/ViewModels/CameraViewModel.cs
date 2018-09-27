using MvvmCross.Core.ViewModels;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using TestProject.Core.Services.Interfaces;
using TestProject.Core.Models;
using Chance.MvvmCross.Plugins.UserInteraction;

namespace TestProject.Core.ViewModels
{
	public class CameraViewModel : BaseViewModel
	{
		private bool isCameraAccessGranted;
		private readonly IPermissionsService permissionsService;
		private readonly ICarnetService carnetService;
		private readonly IUserInteraction userInteraction;

		public CameraViewModel(IPermissionsService permissionsService
							  , ICarnetService carnetService
		                      , IUserInteraction userInteraction)
		{
			this.userInteraction = userInteraction;
			this.carnetService = carnetService;
			this.permissionsService = permissionsService;
			CaptureCommand = new MvxAsyncCommand<byte[]>(Capture);
		}

		public IMvxAsyncCommand<byte[]> CaptureCommand { get; }

		public bool IsCameraAccessGranted
		{
			get => isCameraAccessGranted;
			set => SetProperty(ref isCameraAccessGranted, value);
		}

		public async override Task Initialize()
		{
			await CheckPermissions();
		}

		private async Task CheckPermissions()
		{
			var cameraPermission = await permissionsService.PreCheckPermissionsAccessAsync(Permission.Camera);
			if (cameraPermission == PermissionStatus.Granted)
			{
				IsCameraAccessGranted = true;
				return;
			}

			var result = await permissionsService.CheckPermissionsAccesGrantedAsync(Permission.Camera);
			if (result)
			{
				IsCameraAccessGranted = true;
				return;
			}
		}

		private async Task Capture(byte[] item)
		{
			if (item == null || item.Length == 0)
			{
				return;
			}

			IsBusy = true;
			var result = await carnetService.TryDetectModel(item);
			IsBusy = false;

			if (!result.OperationSucceeded)
			{
				await userInteraction.AlertAsync("Something went wrong");
				return;
			}

			await NavigationService.Navigate<ResultViewModel, CarModel>(result.Value);
		}
	}
}
