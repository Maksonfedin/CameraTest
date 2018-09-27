using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;
using TestProject.Core.ViewModels;
using UIKit;
using TestProject.Core.Models;
using Foundation;
using CoreGraphics;

namespace TestProject.iOS.Views
{
	[MvxChildPresentation]
	public class ResultView : ViewBase<ResultViewModel>
	{
		private UIView whiteBottomView;
		private UIImageView carImage;
		private UILabel carMakeLabel, carModelLabel, carProbabilityLabel;
		private UIButton doneButton;

		public ResultView()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SetViews();
			SetBindings();
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
			carImage.LayoutIfNeeded();
			carImage.Image = UIImage.LoadFromData(NSData.FromArray(carModel.Bytes));

			var imageViewHeight = carImage.Frame.Height;
			var imageViewWidth = carImage.Frame.Width;

			var tlX = imageViewWidth * carModel.Rectangle.TlX;
			var tlY = imageViewHeight * carModel.Rectangle.TlY;
			var brX = imageViewWidth * carModel.Rectangle.BrX;
			var brY = imageViewHeight * carModel.Rectangle.BrY;

			var widht = brX - tlX;
			var height = brY - tlY;

			var view = new UIView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.Clear,
				Layer =
				{
					BorderWidth = 2f,
					BorderColor = UIColor.Yellow.CGColor
				}
			};

			view.Frame = new CGRect(tlX, tlY, widht, height);
			carImage.AddSubview(view);
		}

		private void SetViews()
		{
			carImage = new UIImageView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ClipsToBounds = true,
				ContentMode = UIViewContentMode.ScaleAspectFill
			};

			whiteBottomView = new UIView
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.White
			};

			carMakeLabel = new UILabel
			{
				Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular),
				TextColor = UIColor.Black,
				TextAlignment = UITextAlignment.Left,
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			carModelLabel = new UILabel
			{
				Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular),
				TextColor = UIColor.Black,
				TextAlignment = UITextAlignment.Left,
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			carProbabilityLabel = new UILabel
			{
				Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Regular),
				TextColor = UIColor.Black,
				TextAlignment = UITextAlignment.Left,
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			doneButton = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				Layer =
				{
					BorderWidth = 1f,
					BorderColor = UIColor.Black.CGColor,
					CornerRadius = 6f
				}
			};
			doneButton.SetTitle("Done", UIControlState.Normal);
			doneButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

			whiteBottomView.AddSubviews(carMakeLabel, carModelLabel,
										carProbabilityLabel, doneButton);
			whiteBottomView.AddConstraints(
				doneButton.Bottom().EqualTo(-20f).BottomOf(whiteBottomView),
				doneButton.WithSameCenterX(whiteBottomView),
				doneButton.Height().EqualTo(44f),
				doneButton.Width().EqualTo(150f),

				carMakeLabel.AtTopOf(whiteBottomView, 25f),
				carMakeLabel.AtLeftOf(whiteBottomView, 16f),
				carMakeLabel.AtRightOf(whiteBottomView, 16f),

				carModelLabel.Below(carMakeLabel, 4f),
				carModelLabel.AtLeftOf(whiteBottomView, 16f),
				carModelLabel.AtRightOf(whiteBottomView, 16f),

				carProbabilityLabel.Below(carModelLabel, 4f),
				carProbabilityLabel.AtLeftOf(whiteBottomView, 16f),
				carProbabilityLabel.AtRightOf(whiteBottomView, 16f)
			);

			View.AddSubviews(carImage, whiteBottomView);
			View.AddConstraints(
				carImage.AtTopOf(View),
				carImage.AtLeftOf(View),
				carImage.AtRightOf(View),
				carImage.Height().EqualTo(400f),

				whiteBottomView.AtBottomOf(View),
				whiteBottomView.AtLeftOf(View),
				whiteBottomView.AtRightOf(View),
				whiteBottomView.Below(carImage)
			);
		}

		private void SetBindings()
		{
			var set = this.CreateBindingSet<ResultView, ResultViewModel>();
			set.Bind(doneButton).To(vm => vm.CloseCommand);
			set.Bind(carMakeLabel).To(vm => vm.CarMake);
			set.Bind(carModelLabel).To(vm => vm.CarModel);
			set.Bind(carProbabilityLabel).To(vm => vm.CarProbability);
			set.Bind(this).For(nameof(CarModelDetails)).To(vm => vm.CarModelDetails);
			set.Apply();
		}
	}
}
