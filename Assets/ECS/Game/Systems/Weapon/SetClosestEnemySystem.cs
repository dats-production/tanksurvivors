using System;
using System.Diagnostics.CodeAnalysis;
using DataBase.Game;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using ECS.Views.Impls;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class SetClosestEnemySystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private readonly PlayerConfigSettings _playerConfigSettings;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent>  _enemiesAll;
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent>  _enemies;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private readonly EcsFilter<GameStageComponent> _gameStage;

    private float minDistance = 9999;
    private EcsEntity closestEntity;
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        foreach (var e in _enemiesAll)
        {
            _enemiesAll.GetEntity(e).Del<ClosestEnemyComponent>();
        }

        if(_enemies.GetEntitiesCount()==0) return;
        
        var playerTr = _player.Get1(0).View.Transform;
        foreach (var e in _enemies)
        {
            var enemyTr = _enemies.Get1(e).View.Transform;
            var distance = Vector3.Distance(playerTr.position, enemyTr.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEntity = _enemies.GetEntity(e);
            }
        }
        closestEntity.Get<ClosestEnemyComponent>();
        minDistance = 9999;
    }
}

public struct ClosestEnemyComponent : IEcsIgnoreInFilter
{
        
}