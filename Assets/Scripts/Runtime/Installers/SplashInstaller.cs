using Game.Ui.SplashScreen.Impls;
using Initializers;
using Zenject;

namespace Installers
{
    public class SplashInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SplashScreenWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<SplashInitializer>().AsSingle();
        }
    }
}