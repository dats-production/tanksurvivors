using Game.SceneLoading;
using Runtime.Managers;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using SimpleUi.Abstracts;
using SimpleUi.Interfaces;
using SimpleUi.Signals;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Utils.UiExtensions;
using Zenject;

namespace Runtime.Game.Ui.Windows.MainMenu
{
    public class MainMenuViewController : UiController<MainMenuView>, IInitializable, IDefaultSelectable
    {
        private readonly SignalBus _signalBus;
        private readonly ISceneLoadingManager _sceneLoadingManager;
        private readonly IGameDataManager _gameDataManager;
        private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;

        public Selectable DefaultSelectable => View.newGame;

        public MainMenuViewController(SignalBus signalBus, ISceneLoadingManager sceneLoadingManager,
            IGameDataManager gameDataManager, ICommonPlayerDataService<CommonPlayerData> playerData)
        {
            _signalBus = signalBus;
            _sceneLoadingManager = sceneLoadingManager;
            _gameDataManager = gameDataManager;
            _playerData = playerData;
        }

        public void Initialize()
        {
            View.newGame.OnClickAsObservable().Subscribe(x => OnNewGame()).AddTo(View.newGame);
            var playerData = _playerData.GetData();
            var timer = playerData.Timer;
            View.timer.text = timer.ToString();
            var enemyKilled = playerData.EnemyKilled;
            View.enemyKilled.text = enemyKilled.ToString();
        }

        private void OnNewGame()
        {
            _sceneLoadingManager.LoadScene("Game");
        }

    }
}