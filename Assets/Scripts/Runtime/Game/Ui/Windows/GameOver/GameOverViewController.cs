using DataBase.Game;
using DataBase.Timer;
using ECS.Game.Components;
using ECS.Utils.Extensions;
using Game.SceneLoading;
using Leopotam.Ecs;
using Signals;
using SimpleUi.Abstracts;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scripts.Runtime.Game.Ui.Windows.GameOver 
{
    public class GameOverViewController : UiController<GameOverView> , IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly ISceneLoadingManager _sceneLoadingManager;
        private readonly EcsWorld _world;
    
        public GameOverViewController(SignalBus signalBus, ISceneLoadingManager sceneLoadingManager, EcsWorld world)
        {
            _signalBus = signalBus;
            _sceneLoadingManager = sceneLoadingManager;
            _world = world;
        }
        
        public void Initialize()
        {
            _signalBus.GetStream<SignalEnemyKilled>().Subscribe(x => OnEnemyKilled(x.Value)).AddTo(View);
            _signalBus.GetStream<SignalTimer>().Subscribe(x => OnTimer(x.Value)).AddTo(View);
            View.again.OnClickAsObservable().Subscribe(x => OnNewGame()).AddTo(View.again);
        }

        
        public override void OnShow()
        {
            _world.GetEntity<GameStageComponent>().Get<GameStageComponent>().Value = EGameStage.Pause;
            Amplitude.Instance.logEvent("level_failed");
        }
        
        private void OnNewGame()
        {
            _sceneLoadingManager.ReloadScene();
        }

        private void OnTimer(Timer value)
        {
            View.timer.text = value.ToString();
        }

        private void OnEnemyKilled(int value)
        {
            View.enemyKilled.text = value.ToString();
        }
    }
}