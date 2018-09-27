using System;
using Android.Runtime;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace Chance.MvvmCross.Plugins.UserInteraction.Droid
{
	[Preserve(AllMembers = true)]
	public class Plugin : IMvxPlugin
	{
		public void Load() 
		{
			Mvx.RegisterType<IUserInteraction, UserInteraction>();
		}
	}
}

