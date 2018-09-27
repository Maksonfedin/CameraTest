using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;

namespace TestProject.Droid.Views.Controls
{
	[Register("testproject.CameraView")]
	public class CameraView : SurfaceView, ISurfaceHolderCallback
	{
		Android.Hardware.Camera camera;

		public CameraView(Context context, Android.Hardware.Camera camera) : base(context)
		{
			InitView(camera);
		}

		private void InitView(Android.Hardware.Camera camera)
		{
			this.camera = camera;
			camera.SetDisplayOrientation(90);
			Holder.AddCallback(this);
			Holder.SetType(SurfaceType.PushBuffers);
		}

		public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
		{
			if (Holder.Surface == null)
			{
				return;
			}

			try
			{
				camera.StopPreview();
			}
			catch (Exception)
			{
				// ignore: tried to stop a non-existent preview
			}

			try
			{
				// start preview with new settings
				camera.SetPreviewDisplay(Holder);
				camera.StartPreview();
			}
			catch (Exception e)
			{

			}
		}

		public void SurfaceCreated(ISurfaceHolder holder)
		{
			try
			{
				camera.SetPreviewDisplay(holder);
				camera.StartPreview();
			}
			catch (Exception e)
			{

			}
		}

		public void SurfaceDestroyed(ISurfaceHolder holder)
		{

		}
	}
}
