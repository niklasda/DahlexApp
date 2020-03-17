using DahlexApp.Logic.Interfaces;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace DahlexApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            typeof(IGameEngine).Assembly.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            // todo configure scanner to do this auto

     //       Mvx.IoCProvider.ConstructAndRegisterSingleton<IGameService, GameService>();

            // register the appstart object
            RegisterCustomAppStart<AppStart>();
        }
    }
}
