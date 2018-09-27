using Android.Graphics;
using Android.OS;
using Android.Widget;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Plugin.Permissions;

namespace TestProject.Droid.Views.Activities
{
	public abstract class BaseActivity<TViewModel> : MvxAppCompatActivity<TViewModel>
		where TViewModel : MvxViewModel
	{
		protected abstract int LayoutId { get; }

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(LayoutId);

			FindViewById<ProgressBar>(Resource.Id.progress)?.IndeterminateDrawable?.SetColorFilter(Color.ParseColor("#ffffff"), PorterDuff.Mode.SrcIn);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}