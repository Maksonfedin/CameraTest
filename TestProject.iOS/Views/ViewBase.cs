using System;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using TestProject.Core.ViewModels;
using UIKit;

namespace TestProject.iOS.Views
{
	public class ViewBase<T> : MvxViewController<T> where T : BaseViewModel
	{
		public ViewBase()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			AutomaticallyAdjustsScrollViewInsets = false;
			View.BackgroundColor = UIColor.White;
		}
	}
}
