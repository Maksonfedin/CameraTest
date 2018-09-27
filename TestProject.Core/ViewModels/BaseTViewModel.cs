using MvvmCross.Core.ViewModels;

namespace TestProject.Core.ViewModels
{
	public abstract class BaseViewModel<TParam> : BaseViewModel, IMvxViewModel<TParam>
	{
		public abstract void Prepare(TParam parameter);

		public BaseViewModel()
		{
		}
	}
}
