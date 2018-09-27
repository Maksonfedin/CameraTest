using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace TestProject.Core
{
	public class App : MvvmCross.Core.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			RegisterAppStart(new AppStart());
		}
	}
}
