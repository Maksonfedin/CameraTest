using System;
using UIKit;
using AVFoundation;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views.Presenters.Attributes;
using TestProject.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;

namespace TestProject.iOS.Views
{
	[MvxRootPresentation(WrapInNavigationController = true)]
	public class CameraView : ViewBase<CameraViewModel>
	{
		private UIView liveCameraStream;
		private UIButton captureButton;
		private UIButton cancelButton;

		private UIActivityIndicatorView activityView;
		private bool isBusy;

		private bool isCameraAccessGranted;
		private AVCaptureSession captureSession;
		private AVCaptureDeviceInput captureDeviceInput;
		private AVCaptureStillImageOutput stillImageOutput;
		private AVCaptureVideoPreviewLayer videoPreviewLayer;

		public CameraView()
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SetViews();
			SetBindings();
		}

		private void SetViews()
		{
			liveCameraStream = new UIView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			captureButton = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.White
			};
			captureButton.SetTitle("Capture", UIControlState.Normal);
			captureButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			captureButton.TouchUpInside += CaptureButtonClick;

			cancelButton = new UIButton
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				BackgroundColor = UIColor.White
			};
			cancelButton.SetTitle("Cancel", UIControlState.Normal);
			cancelButton.SetTitleColor(UIColor.Black, UIControlState.Normal);

			activityView = new UIActivityIndicatorView
			{
				ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge,
				BackgroundColor = UIColor.Black.ColorWithAlpha(0.2f),
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			View.AddSubviews(liveCameraStream, captureButton, cancelButton, activityView);
			View.AddConstraints(
				liveCameraStream.AtTopOf(View),
				liveCameraStream.AtLeftOf(View),
				liveCameraStream.AtRightOf(View),
				liveCameraStream.AtBottomOf(View),

				captureButton.Bottom().EqualTo(-20f).BottomOf(View),
				captureButton.Leading().EqualTo(20f).LeadingOf(View),
				captureButton.Trailing().EqualTo(-10f).LeadingOf(cancelButton),
				captureButton.Height().EqualTo(44f),
				cancelButton.Width().EqualTo(150f),

				cancelButton.Leading().EqualTo(10f).TrailingOf(captureButton),
				cancelButton.Trailing().EqualTo(-20f).TrailingOf(View),
				cancelButton.Height().EqualTo(44f),
				cancelButton.WithSameWidth(captureButton),
				cancelButton.Bottom().EqualTo().BottomOf(captureButton),

				activityView.AtTopOf(View),
				activityView.Leading().EqualTo().LeadingOf(View),
				activityView.Trailing().EqualTo().TrailingOf(View),
				activityView.AtBottomOf(View));

		}

		private void SetBindings()
		{
			var set = this.CreateBindingSet<CameraView, CameraViewModel>();
			set.Bind(this).For(v => v.IsCameraAccessGranted).To(vm => vm.IsCameraAccessGranted);
			set.Bind(cancelButton).To(vm => vm.CloseCommand);
			set.Bind(this).For(p => p.IsBusy).To(vm => vm.IsBusy);
			set.Apply();
		}

		private async void CaptureButtonClick(object sender, EventArgs args)
		{
			var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
			var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);

			var jpegImageAsNsData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);
			var jpegAsByteArray = jpegImageAsNsData.ToArray();

			ViewModel.CaptureCommand?.Execute(jpegAsByteArray);
		}

		private void ShowCameraPreview()
		{
			var captureSession = new AVCaptureSession();

			var viewLayer = liveCameraStream.Layer;
			videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
			{
				Frame = this.View.Frame
			};
			liveCameraStream.Layer.AddSublayer(videoPreviewLayer);

			var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
			ConfigureCameraForDevice(captureDevice);
			captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
			captureSession.AddInput(captureDeviceInput);

			var dictionary = new NSMutableDictionary();
			dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
			stillImageOutput = new AVCaptureStillImageOutput()
			{
				OutputSettings = new NSDictionary()
			};

			captureSession.AddOutput(stillImageOutput);
			captureSession.StartRunning();

			captureButton.Hidden = false;
			cancelButton.Hidden = false;
		}

		private void ConfigureCameraForDevice(AVCaptureDevice device)
		{
			if (device == null)
			{
				return;
			}

			var error = new NSError();
			if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
			{
				device.LockForConfiguration(out error);
				device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
				device.UnlockForConfiguration();
			}
			else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
			{
				device.LockForConfiguration(out error);
				device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
				device.UnlockForConfiguration();
			}
			else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
			{
				device.LockForConfiguration(out error);
				device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
				device.UnlockForConfiguration();
			}
		}

		private void HideCameraPreview()
		{
			if (captureSession != null)
			{
				captureSession.StopRunning();
				captureSession.RemoveOutput(stillImageOutput);
				captureSession.Dispose();
				captureSession = null;
			}

			if (stillImageOutput != null)
			{
				stillImageOutput.Dispose();
				stillImageOutput = null;
			}

			captureButton.Hidden = true;
			cancelButton.Hidden = true;
		}

		public bool IsBusy
		{
			get => isBusy;
			set
			{
				isBusy = value;

				if (value)
				{
					activityView.Hidden = false;
				}
				else
					activityView.Hidden = true;
			}
		}

		public bool IsCameraAccessGranted
		{
			get => isCameraAccessGranted;
			set
			{
				isCameraAccessGranted = value;

				if (value)
				{
					ShowCameraPreview();
				}
				else
				{
					HideCameraPreview();
				}
			}
		}
	}
}
