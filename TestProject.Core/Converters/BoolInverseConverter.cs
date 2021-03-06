﻿using System;
using MvvmCross.Platform.Converters;

namespace TestProject.Core.Converters
{
	public class BoolInverseConverter : MvxValueConverter<bool, bool>
	{
		protected override bool Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !value;
		}
	}
}
