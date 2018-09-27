using System;
using MvvmCross.Platform.Converters;
using Android.Views;

namespace TestProject.Droid.Converters
{
	public class BoolToGoneConverter : MvxValueConverter<bool, ViewStates>
	{
		protected override ViewStates Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value ? ViewStates.Visible : ViewStates.Gone;
		}
	}
}
