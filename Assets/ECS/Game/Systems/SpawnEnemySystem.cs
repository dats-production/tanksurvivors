using System;
using System.Diagnostics.CodeAnalysis;
using DataBase.Game;
using DataBase.Objects;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Systems.Move;
using ECS.Utils.Extensions;
using ECS.Views.Impls;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Services.Uid;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class SpawnEnemySystem : ReactiveSystem<SpawnEnemyComponent>
{
    [Inject] private readonly GetPointFromScene _getPointFromScene;
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent>.Exclude<IsAvailableComponent>  _enemies;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    protected override EcsFilter<SpawnEnemyComponent> ReactiveFilter { get; }   
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        
        
        foreach (var e in _enemies)
        {
            var pos = entity.Get<SpawnEnemyComponent>().Pos;
            var enemy = entity.Get<SpawnEnemyComponent>().Enemy;
            var enemyView = _enemies.Get1(e).Get<EnemyView>();
            enemyView.SetEnemyData(enemy);
            var enemyEntity = _enemies.GetEntity(e);
            enemyEntity.Get<PositionComponent>().Value = pos;            
            enemyEntity.GetAndFire<IsAvailableComponent>();
            return;
        }
        entity.Destroy();
    }
}  


