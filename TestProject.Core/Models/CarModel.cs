using System;
namespace TestProject.Core.Models
{
	public class CarModel
	{
		public byte[] Bytes { get; set; }

		public string MakeName { get; set; }

		public string ModelName { get; set; }

		public double Probability { get; set; }

		public string Error { get; set; }

		public BoundingBox Rectangle { get; set; }
	}
}
