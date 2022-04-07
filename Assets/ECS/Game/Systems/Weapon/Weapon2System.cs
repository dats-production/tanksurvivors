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

public class Weapon2System : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent>  _enemies;
    private EcsFilter<LinkComponent, Weapon2Component>  _w2;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private EcsFilter<LinkComponent, MissileComponent>.Exclude<IsAvailableComponent> _missiles;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    float timer;
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        if (_w2.IsEmpty()) return;
        
        timer += Time.deltaTime; 
        var fireRate = _config.WeaponsCfg.w2.fireRate;        
        if (!(timer >= fireRate)) return;
        var missileReleased = 0;
        var projectileCountAdd = _w2.GetEntity(0).Get<ProjectileCountAddComponent>().Value;
        foreach (var e in _enemies)
        {
            var enemy = _enemies.GetEntity(e);
            var enemyTr = _enemies.Get1(e).View.Transform;
            var w2View = _w2.Get1(0).Get<Weapon2View>();
            if(w2View.GetAttackRange()<Vector3.Distance(w2View.Transform.position, enemyTr.position)) continue;
            var muzzleTr = w2View.muzzle;
            SpawnMissile(muzzleTr, enemy);
            missileReleased++;
            if(missileReleased >= _config.WeaponsCfg.w2.missileCountPerShot + projectileCountAdd)
                break;
        }
        timer=0;
    }

    private void SpawnMissile(Transform muzzle, EcsEntity enemy)
    {
        var projectileSpeedAdd = _w2.GetEntity(0).Get<ProjectileSpeedAddComponent>().Value;
        foreach (var b in _missiles)
        {          
            var missileEntity = _missiles.GetEntity(b);
            missileEntity.GetAndFire<IsAvailableComponent>();
            var missileView = _missiles.Get1(b).Get<MissileView>();
            missileEntity.Del<PositionComponent>();
            missileView.Transform.position = muzzle.position;
            missileView.Transform.rotation = muzzle.rotation;
            missileEntity.Get<MissileTargetComponent>().Enemy = enemy; //muzzle.position + muzzle.forward * _config.WeaponsCfg.w1.bulletFlyDistance; 
            missileEntity.Get<MissileTargetComponent>().Speed = _config.WeaponsCfg.w2.missileSpeed * (1+projectileSpeedAdd);
            return;
        }
    }
}

public struct MissileComponent : IEcsIgnoreInFilter
{
    
}

public struct Weapon2Component : IEcsIgnoreInFilter
{
    
}    


