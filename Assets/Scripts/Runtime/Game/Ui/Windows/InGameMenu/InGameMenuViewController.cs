using ECS.Game.Components.Events;
using Game.SceneLoading;
using Game.Ui.BlackScreen;
using Leopotam.Ecs;
using Services.PauseService;
using Signals;
using SimpleUi.Abstracts;
using SimpleUi.Signals;
using UniRx;
using Zenject;

namespace Game.Ui.InGameMenu
{
    public class InGameMenuViewController : UiController<InGameMenuView>, IInitializable
    {
        private readonly ISceneLoadingManager _sceneLoadingManager;
        private readonly EcsWorld _world;
        private readonly SignalBus _signalBus;
        private readonly IPauseService _pauseService;

        public InGameMenuViewController(ISceneLoadingManager sceneLoadingManager, EcsWorld world, SignalBus signalBus, IPauseService pauseService)
        {
            _sceneLoadingManager = sceneLoadingManager;
            _world = world;
            _signalBus = signalBus;
            _pauseService = pauseService;
        }
        
        public void Initialize()
        {
            View.GoMenu.OnClickAsObservable().Subscribe(x => OnGoMenu()).AddTo(View.GoMenu);
            View.Continue.OnClickAsObservable().Subscribe(x => OnContinue()).AddTo(View.Continue);
            View.SaveGame.OnClickAsObservable().Subscribe(x => OnSaveGame()).AddTo(View.SaveGame);
        }

        public override void OnShow()
        {
            _pauseService.PauseGame(true);
            View.GoMenu.Select();
            View.GoMenu.OnSelect(null);
        }

        private void OnContinue()
        {
            _pauseService.PauseGame(false);
            _signalBus.BackWindow();
        }

        private void OnSaveGame()
        {
            _world.NewEntity().Get<SaveGameEventComponent>();
        }
        
        private void OnGoMenu()
        {
            _signalBus.BackWindow();
            _signalBus.OpenWindow<BlackScreenWindow>(EWindowLayer.Project);
            _signalBus.Fire(new SignalBlackScreen(false, () =>
            {
                _sceneLoadingManager.LoadScene("MainMenu");
            }));
        }
    }
}