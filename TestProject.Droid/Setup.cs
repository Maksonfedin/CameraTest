using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Content;
using Chance.MvvmCross.Plugins.UserInteraction;
using Chance.MvvmCross.Plugins.UserInteraction.Droid;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using TestProject.Core.Converters;

namespace TestProject.Droid
{
	public class Setup : MvxAndroidSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}

		protected override IMvxTrace CreateDebugTrace()
		{
			return new DebugTrace();
		}

		protected override void InitializePlatformServices()
		{
			base.InitializePlatformServices();

			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			Mvx.RegisterSingleton<IUserInteraction>(() => new UserInteraction());
		}

		protected override IEnumerable<Assembly> ValueConverterAssemblies
		{
			get
			{
				var toReturn = base.ValueConverterAssemblies.ToList();
				toReturn.Add(typeof(BoolInverseConverter).Assembly);
				toReturn.Add(typeof(Setup).Assembly);
				return toReturn;
			}
		}

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			base.FillTargetFactories(registry);
			MvxAppCompatSetupHelper.FillTargetFactories(registry);
		}

		protected override void FillValueConverters(IMvxValueConverterRegistry registry)
		{
			base.FillValueConverters(registry);
			foreach (var assembly in ValueConverterAssemblies)
				foreach (var item in assembly.CreatableTypes().EndingWith("Converter"))
					registry.AddOrOverwrite(item.Name, (IMvxValueConverter)Activator.CreateInstance(item));
		}

		protected override IMvxAndroidViewPresenter CreateViewPresenter()
		{
			return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
		}
	}
}
