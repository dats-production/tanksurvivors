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
using Runtime.Game.Ui;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Services.Uid;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ExplosionDamageSystem : ReactiveSystem<ExplosionDamageComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent> _enemies;
    private EcsFilter<ExplosionDamageComponent> _expls;
    private EcsFilter<Weapon1Component> _w1;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<ExplosionDamageComponent> ReactiveFilter { get; }    
    
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        
        var explView = entity.Get<LinkComponent>().View as IDamager;
        var explPos = entity.Get<ExplosionDamageComponent>().explPos;
        var explRadius = entity.Get<ExplosionDamageComponent>().explRadius;
        var damageAdd = entity.Get<ExplosionDamageComponent>().damageAdd;
        foreach (var e in _enemies)
        {
            var enemyView = _enemies.Get1(e).View as EnemyView;
            if (!(Vector3.Distance(explPos, enemyView.Center.position) < enemyView.GetTriggerDistance() + explRadius)) continue;
            if(enemyView)
            {
                _enemies.GetEntity(e).Get<DamageUIEventComponent>().position = enemyView.Transform.position;
                _enemies.GetEntity(e).Get<DamageUIEventComponent>().damage = explView.Damage + damageAdd;
                explView.DealDamage(enemyView, damageAdd);
            }
        }
    }
}
public struct ExplosionDamageComponent : IEcsIgnoreInFilter
{
    public Vector3 explPos;    
    public float explRadius;
    public float damageAdd;
}

