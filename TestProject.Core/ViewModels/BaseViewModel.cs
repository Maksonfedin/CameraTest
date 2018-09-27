using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace TestProject.Core.ViewModels
{
	public class BaseViewModel : MvxViewModel, IMvxViewModel
	{
		private bool isBusy;

		public BaseViewModel()
		{
			CloseCommand = new MvxCommand(OnClose);
		}

		protected IMvxNavigationService NavigationService => Mvx.Resolve<IMvxNavigationService>();

		public IMvxCommand CloseCommand { get; }

		public bool IsBusy
		{
			get => isBusy;
			set => SetProperty(ref isBusy, value);
		}

		protected virtual void OnClose()
		{
			Close(this);
		}
	}
}
