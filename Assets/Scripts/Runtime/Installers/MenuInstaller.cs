using Initializers;
using Runtime.Game.Ui.Windows.MainMenu;
using Runtime.UI.QuitConcentPopUp;
using Zenject;

namespace Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuInitializer>().AsSingle();
            BindWindows();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<MainMenuWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConsentWindow>().AsSingle();
        }
    }
}