using System.Collections.Generic;
using DataBase.Game;
using ECS.DataSave;
using ECS.Utils.Extensions;
using Leopotam.Ecs;
using PdUtils.Interfaces;
using Runtime.Game.Ui;
using Runtime.Game.Ui.Windows.TouchPad;
using Runtime.Services.GameStateService;
using SimpleUi.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.Initializers
{
    public class GameInitializer : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly EcsWorld _world;
        private readonly ITouchpadViewController _touchpadViewController;
        private readonly IGameStateService<GameState> _gameState;
        private readonly IList<IUiInitializable> _uiInitializables; //late initialize after ecs init

        public GameInitializer(SignalBus signalBus, ITouchpadViewController touchpadViewController,
            IList<IUiInitializable> uiInitializables, IGameStateService<GameState> gameState)
        {
            _signalBus = signalBus;
            _touchpadViewController = touchpadViewController;
            _uiInitializables = uiInitializables;
            _gameState = gameState;
        }

        public void Initialize()
        {
            foreach (var ui in _uiInitializables)
                ui.Initialize();
            _signalBus.OpenWindow<GameHudWindow>();
            _touchpadViewController.SetActive(true);
        }
    }
}