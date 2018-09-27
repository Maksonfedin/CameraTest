using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using TestProject.Core.ViewModels;
using TestProject.Droid.Views.Controls;

namespace TestProject.Droid.Views.Activities
{
	[Activity(ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/AppTheme")]
	public class CameraActivity : BaseActivity<CameraViewModel>, Android.Hardware.Camera.IPictureCallback, Android.Hardware.Camera.IAutoFocusCallback
	{
		private LinearLayout root;
		private Android.Hardware.Camera camera;
		private bool cameraReleased;
		private Button caprtureButton;

		protected override int LayoutId => Resource.Layout.camera_activity;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			root = FindViewById<LinearLayout>(Resource.Id.root);

			caprtureButton = FindViewById<Button>(Resource.Id.bCapture);
			caprtureButton.Click += CaptureClicked;

			InitCamera();
		}

		protected override void OnDestroy()
		{
			camera.StopPreview();
			camera.Release();
			cameraReleased = true;

			root.Click -= AutofocusCalled;
			caprtureButton.Click -= CaptureClicked;
			base.OnDestroy();
		}

		protected override void OnResume()
		{
			if (cameraReleased)
			{
				camera.Reconnect();
				camera.StartPreview();
				cameraReleased = false;
			}
			base.OnResume();
		}

		public void OnPictureTaken(byte[] data, Android.Hardware.Camera camera)
		{
			try
			{
				ViewModel.CaptureCommand?.Execute(data);
				camera.StartPreview();
			}
			catch (System.Exception e)
			{
			}
		}

		public void OnAutoFocus(bool success, Android.Hardware.Camera camera)
		{

		}

		private Android.Hardware.Camera SetUpCamera()
		{
			Android.Hardware.Camera c = null;
			try
			{
				c = Android.Hardware.Camera.Open();
			}
			catch (Exception e)
			{
			}

			return c;
		}

		private void AutofocusCalled(object sender, EventArgs e)
		{
			camera.AutoFocus(this);
		}

		private void InitCamera()
		{
			camera = SetUpCamera();
			SetCamFocusMode();
			SetCameraPreview();
		}

		private void CaptureClicked(object sender, EventArgs e)
		{
			try
			{
				camera.StartPreview();
				camera.TakePicture(null, null, this);
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		private void SetCamFocusMode()
		{
			if (camera == null)
			{
				return;
			}
			var parameters = camera.GetParameters();
			List<String> focusModes = parameters.SupportedFocusModes.ToList();
			if (focusModes.Contains(Android.Hardware.Camera.Parameters.FocusModeContinuousPicture))
			{
				parameters.FocusMode = Android.Hardware.Camera.Parameters.FocusModeContinuousPicture;
			}
			else if (focusModes.Contains(Android.Hardware.Camera.Parameters.FocusModeAuto))
			{
				parameters.FocusMode = Android.Hardware.Camera.Parameters.FocusModeAuto;
			}
			camera.SetParameters(parameters);

			root.Click += AutofocusCalled;
		}

		private void SetCameraPreview()
		{
			root.AddView(new CameraView(this, camera));
		}
	}
}
