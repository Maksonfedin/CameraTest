using System;
using static DetectModelResponse.Types.DetectedObject.Types;

namespace TestProject.Core.Models
{
	public class BoundingBox
	{
		public BoundingBox() 
		{

		}

		public BoundingBox(BBox bBox)
		{
			TlX = bBox.TlX;
			TlY = bBox.TlY;
			BrX = bBox.BrX;
			BrY = bBox.BrY;
		}

		public double TlX { get; set; }

		public double TlY { get; set; }

		public double BrX { get; set; }

		public double BrY { get; set; }
	}
}
