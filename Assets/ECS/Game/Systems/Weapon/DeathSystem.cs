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
using ECS.Views;
using ECS.Views.Impls;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Services.Uid;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class DeathSystem : ReactiveSystem<DeathComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, ExperienceComponent>.Exclude<IsAvailableComponent>  _exps;
    private EcsFilter<PlayerComponent>  _player;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<DeathComponent> ReactiveFilter { get; }    
    
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var enemyView = entity.Get<LinkComponent>().View;
        var enemyTr = enemyView.Transform;
        var expValue = entity.Get<DeathComponent>().expFromEnemy;
        if(Random.Range(0,4)==0 && expValue <= 50)
        {
            SpawnExperiens(enemyTr, expValue);
        }
        else if (expValue > 50)
        {
            SpawnExperiens(enemyTr, expValue);
        }
        
        //entity.DelAndFire<IsAvailableComponent>();

        EnemyKilledCount();
    }

    private void SpawnExperiens(Transform enemy, int expValue)
    {
        foreach (var exp in _exps)
        {
            var expEntity = _exps.GetEntity(exp);
            expEntity.Get<PositionComponent>().Value = enemy.position;
            expEntity.Get<ExperienceComponent>().Value = expValue;
            expEntity.GetAndFire<IsAvailableComponent>();
            return;
        }
    }
    
    private void EnemyKilledCount()
    {
        ref var enemyKilled = ref _player.GetEntity(0).Get<EnemyKilledComponent>().Value;
        enemyKilled++;
        _signalBus.Fire(new SignalEnemyKilled(enemyKilled));
        var playerData = _playerData.GetData();        
        if (enemyKilled > playerData.EnemyKilled)
        {
            playerData.EnemyKilled = enemyKilled;
            _playerData.Save(playerData);
        }
    }
}
public struct DeathComponent
{
    public int expFromEnemy;
}
public struct EnemyKilledComponent
{
    public int Value;
}

