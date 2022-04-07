using System;
using System.Diagnostics.CodeAnalysis;
using DataBase.Game;
using DataBase.Objects;
using DataBase.Shop;
using DataBase.Shop.Impl;
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

public class Weapon1System : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    [Inject] private readonly IUpgradesBase _upgBase;
    
    private EcsFilter<LinkComponent, EnemyComponent, ClosestEnemyComponent>  _closest;
    private EcsFilter<LinkComponent, Weapon1Component>  _w1;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private EcsFilter<LinkComponent, BulletComponent>.Exclude<IsAvailableComponent> _bullet;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    float timer;
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        if (_w1.IsEmpty()) return;
        if(_closest.IsEmpty()) return;
        var closestTr = _closest.Get1(0).View.Transform;

        var w1View = _w1.Get1(0).Get<Weapon1View>();
        if(w1View.GetAttackRange()<Vector3.Distance(w1View.Transform.position, closestTr.position)) return;
        timer += Time.deltaTime;
        
        var aimSpeedAdd = _w1.GetEntity(0).Get<AimSpeedAddComponent>().Value;

        var cooldownSub = _w1.GetEntity(0).Get<CooldownSubComponent>().Value;
        w1View.AimTurretOnTarget(closestTr, aimSpeedAdd);
        
        if (_config.WeaponsCfg.w1.fireRate - cooldownSub < 0) cooldownSub = _config.WeaponsCfg.w1.fireRate;
        var fireRate = _config.WeaponsCfg.w1.fireRate - cooldownSub;
        if (!(timer >= fireRate)) return;

        var muzzle = w1View.muzzle;
        SpawnBullet(muzzle);
        w1View.EnableLight();
        
        timer=0;
    }

    private void SpawnBullet(Transform muzzle)
    {
        var projectileCountAdd = _w1.GetEntity(0).Get<ProjectileCountAddComponent>().Value;
        var projectileSpeedAdd = _w1.GetEntity(0).Get<ProjectileSpeedAddComponent>().Value;
        for (int i = 0; i < _config.WeaponsCfg.w1.bulletCountPerShot + projectileCountAdd; i++)
        {
            foreach (var b in _bullet)
            {
                var bulletEntity = _bullet.GetEntity(b);
                bulletEntity.GetAndFire<IsAvailableComponent>();
                //var bullerView = _bullet.Get1(b).Get<BulletView>();
                var pos = muzzle.position - muzzle.transform.forward * 6 * i * _config.WeaponsCfg.w1.bulletRadius;
                bulletEntity.Get<PositionComponent>().Value = pos;
                bulletEntity.Get<RotationComponent>().Value = muzzle.rotation; //Quaternion.LookRotation(closestPos-pos);
                bulletEntity.Get<TargetPositionComponent>().Value
                    = muzzle.position + muzzle.forward * _config.WeaponsCfg.w1.bulletFlyDistance;
                bulletEntity.Get<TargetPositionComponent>().Speed = _config.WeaponsCfg.w1.bulletSpeed + projectileSpeedAdd;
                break;
            }
        }
    }
}

public struct BulletComponent : IEcsIgnoreInFilter
{
    
}

public struct Weapon1Component : IEcsIgnoreInFilter
{
    
}   


