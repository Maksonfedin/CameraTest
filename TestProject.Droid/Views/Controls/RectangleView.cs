using Android.Content;
using Android.Graphics;
using Android.Views;

namespace TestProject.Droid.Views.Controls
{
	public class RectangleView : View
	{
		float tlX, tlY, brX, brY;

		public RectangleView(Context context, double tlX, double tlY, double brX, double brY)
			: base(context)
		{
			this.tlX = (float)tlX;
			this.tlY = (float)tlY;
			this.brX = (float)brX;
			this.brY = (float)brY;
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			var paint = new Paint { Color = Color.Yellow, StrokeWidth = 3 };
			paint.SetStyle(Paint.Style.Stroke);
			canvas.DrawRect(new RectF(tlX, tlY, brX, brY), paint);
		}
	}
}
