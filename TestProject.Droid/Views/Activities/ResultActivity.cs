using Android.App;
using Android.Content.PM;
using TestProject.Core.ViewModels;
using Android.Widget;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using TestProject.Core.Models;
using Android.Graphics;
using TestProject.Droid.Views.Controls;

namespace TestProject.Droid.Views.Activities
{
	[Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/AppTheme")]
	public class ResultActivity : BaseActivity<ResultViewModel>
	{
		private ImageView imageViewCar;
		private RelativeLayout relativeLayoutImageRoot;

		protected override int LayoutId => Resource.Layout.result_activity;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			imageViewCar = FindViewById<ImageView>(Resource.Id.imageViewCar);
			relativeLayoutImageRoot = FindViewById<RelativeLayout>(Resource.Id.relativeLayoutImageRoot);

			var set = this.CreateBindingSet<ResultActivity, ResultViewModel>();
			set.Bind(this).For(nameof(CarModelDetails)).To(vm => vm.CarModelDetails);
			set.Apply();
		}

		public CarModel CarModelDetails
		{
			set
			{
				if (value == null)
				{
					return;
				}

				SetupImageOvewlay(value);
			}
		}

		private void SetupImageOvewlay(CarModel carModel)
		{
			var height = (int)(400 * Resources.DisplayMetrics.Density);
			var width = Resources.DisplayMetrics.WidthPixels;

			var bmp = BitmapFactory.DecodeByteArray(carModel.Bytes, 0, carModel.Bytes.Length);
			imageViewCar.SetImageBitmap(Bitmap.CreateScaledBitmap(bmp, Resources.DisplayMetrics.WidthPixels, height, false));

			var tlX = width * carModel.Rectangle.TlX;
			var tlY = height * carModel.Rectangle.TlY;
			var brX = width * carModel.Rectangle.BrX;
			var brY = height * carModel.Rectangle.BrY;

			var rectangle = new RectangleView(this, tlX, tlY, brX, brY);
			relativeLayoutImageRoot.AddView(rectangle);
		}
	}
}
