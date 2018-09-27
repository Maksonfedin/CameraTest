using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace TestProject.Droid.Views
{
	[Activity(
		  MainLauncher = true
		, Theme = "@style/AppTheme"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashActivity : MvxSplashScreenActivity
	{
		public SplashActivity() : base(Resource.Layout.activity_splash)
		{
		}
	}
}
