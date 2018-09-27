using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using TestProject.Core.ViewModels;

namespace TestProject.Core
{
	public class AppStart : MvxNavigatingObject, IMvxAppStart
	{
		public void Start(object hint = null)
		{
			Mvx.Resolve<IMvxNavigationService>().Navigate<CameraViewModel>();
		}
	}
}
