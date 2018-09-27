using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chance.MvvmCross.Plugins.UserInteraction;
using Chance.MvvmCross.Plugins.UserInteraction.Touch;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.IoC;
using MvvmCross.Platform.Platform;
using UIKit;

namespace TestProject.iOS
{
	public class Setup : MvxIosSetup
	{
		public Setup(IMvxApplicationDelegate applicationDelegate, UIWindow window)
			: base(applicationDelegate, window)
		{
		}

		public Setup(IMvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
			: base(applicationDelegate, presenter)
		{
		}

		protected override IEnumerable<Assembly> ValueConverterAssemblies
		{
			get
			{
				var toReturn = base.ValueConverterAssemblies.ToList();
				toReturn.Add(typeof(Setup).Assembly);
				return toReturn;
			}
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

		protected override void FillValueConverters(IMvxValueConverterRegistry registry)
		{
			base.FillValueConverters(registry);

			foreach (var assembly in ValueConverterAssemblies)
			{
				foreach (var item in assembly.CreatableTypes().EndingWith("Converter"))
				{
					registry.AddOrOverwrite(item.Name, (IMvxValueConverter)Activator.CreateInstance(item));
				}
			}
		}

		protected override IMvxIosViewPresenter CreatePresenter()
		{
			return new MvxIosViewPresenter(ApplicationDelegate, Window);
		}

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			base.FillTargetFactories(registry);

		}
	}
}
