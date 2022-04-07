using ECS.Utils.Impls;
using Game.Ui.InGameMenu;
using Runtime.Game.Ui;
using Runtime.Initializers;
using Runtime.UI.QuitConcentPopUp;
using Scripts.Runtime.Game.Ui.Windows.GameOver;
using Scripts.Runtime.Game.Ui.Windows.LevelUp;
using Services.PauseService.Impls;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWindows();
            BindServices();
            Container.BindInterfacesAndSelfTo<GameInitializer>().AsSingle();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<ConsentWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<InGameMenuWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameHudWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelUpWindow>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverWindow>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<SpawnService>().AsSingle();
            Container.BindInterfacesTo<PauseService>().AsSingle();
        }
    }
}