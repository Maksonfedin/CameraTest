using System;
using System.Windows.Input;

namespace Chance.MvvmCross.Plugins.UserInteraction
{
	public class SelectorItem
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public ICommand Command { get; set;}
	}
}

