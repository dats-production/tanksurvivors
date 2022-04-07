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

public class Weapon4System : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent>  _enemies;
    private EcsFilter<LinkComponent, Weapon4Component>  _w4;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private EcsFilter<LinkComponent, MineComponent>.Exclude<IsAvailableComponent> _mines;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    float timer;
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        if (_w4.IsEmpty()) return;
        
        timer += Time.deltaTime; 
        var cooldownSub = _w4.GetEntity(0).Get<CooldownSubComponent>().Value;
        var fireRate = _config.WeaponsCfg.w4.fireRate - cooldownSub;
        if (!(timer >= fireRate)) return;
        var damageAdd = _w4.GetEntity(0).Get<DamageAddComponent>().Value;
        var explosionRadiusAdd = _w4.GetEntity(0).Get<ExplosionAreaAddComponent>().Value;
        var playerPos = _player.Get1(0).View.Transform.position;
        var w4View = _w4.Get1(0).View as Weapon4View;
        _w4.GetEntity(0).Get<ExplosionDamageComponent>().explPos = playerPos;
        _w4.GetEntity(0).Get<ExplosionDamageComponent>().explRadius = w4View.GetAttackRange() * (1 + explosionRadiusAdd);
        _w4.GetEntity(0).Get<ExplosionDamageComponent>().damageAdd = damageAdd;
        timer=0;
    }
}

public struct Weapon4Component : IEcsIgnoreInFilter
{
    
}    


