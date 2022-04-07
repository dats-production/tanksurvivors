using System;
using System.Collections.Generic;
using ECS.Core.Utils.ReactiveSystem;
using ECS.DataSave;
using ECS.Game.Components;
using ECS.Game.Components.Events;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using Leopotam.Ecs;
using Runtime.Services.GameStateService;
using SimpleUi;
using Utils.SeparateThreadExecutor.Impl;
using Zenject;

namespace ECS.Game.Systems
{
    public class SaveGameSystem : ReactiveSystem<SaveGameEventComponent>
    {
        [Inject] private readonly IGameStateService<GameState> _gameState;
        private readonly EcsWorld _world;
        private readonly EcsFilter<UIdComponent> _entities;
        protected override EcsFilter<SaveGameEventComponent> ReactiveFilter { get; }
        protected override void Execute(EcsEntity entity)
        {
            var gameStateData =  _gameState.GetData();
            gameStateData.SaveState = new List<SaveState>();
            
            foreach (var i in _entities)
            {
                var components = new SaveState();
                components.WriteState(_entities.GetEntity(i));
                gameStateData.SaveState.Add(components);
            }
            _gameState.Save();
        }
    }
}