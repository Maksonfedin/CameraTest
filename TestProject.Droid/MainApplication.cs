﻿using System;
using Android.App;
using Android.OS;
using Android.Runtime;

namespace TestProject.Droid
{
	public class MainApplication : Application, Application.IActivityLifecycleCallbacks
	{
		public MainApplication(IntPtr handle, JniHandleOwnership transer)
		  : base(handle, transer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();
			RegisterActivityLifecycleCallbacks(this);
		}

		public override void OnTerminate()
		{
			base.OnTerminate();
			UnregisterActivityLifecycleCallbacks(this);
		}

		public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
		{
		}

		public void OnActivityDestroyed(Activity activity)
		{
		}

		public void OnActivityPaused(Activity activity)
		{
		}

		public void OnActivityResumed(Activity activity)
		{
		}

		public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
		{
		}

		public void OnActivityStarted(Activity activity)
		{
		}

		public void OnActivityStopped(Activity activity)
		{
		}
	}
}
