using System;
using System.Linq;

namespace TestProject.Core.Models
{
	public static class CarModelMapper
	{
		public static CarModel ToCarModel(this DetectModelResponse response)
	{
		if (response == null)
			return null;

		var model = response.DetectedModels[response.SelectedDetectedModelIndex];
		var bbox = new BoundingBox(response.DetectedObjects.FirstOrDefault().Bbox);

		return new CarModel
		{
			MakeName = model?.MakeName,
			ModelName = model?.ModelName,
			Probability = model?.ModelProb ?? 0,
			Rectangle = bbox
		};
	}
}
}
