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

public class Weapon3System : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent>  _enemies;
    private EcsFilter<LinkComponent, Weapon3Component>  _w3;
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private EcsFilter<LinkComponent, MineComponent>.Exclude<IsAvailableComponent> _mines;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    float timer;
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        if (_w3.IsEmpty()) return;
        timer += Time.deltaTime; 
        var cooldownSub = _w3.GetEntity(0).Get<CooldownSubComponent>().Value;
        var fireRate = _config.WeaponsCfg.w3.fireRate - cooldownSub;
        //Debug.Log("Cooldown " + fireRate);
        if (!(timer >= fireRate)) return;

        var w3View = _w3.Get1(0).Get<Weapon3View>();
        var muzzleTr = w3View.muzzle;
        SpawnMine(muzzleTr);

        timer=0;
    }

    private void SpawnMine(Transform muzzle)
    {
        foreach (var m in _mines)
        {
            var mineEntity = _mines.GetEntity(m);
            mineEntity.GetAndFire<IsAvailableComponent>();
            mineEntity.Get<PositionComponent>().Value = muzzle.position;
            return;
        }
    }
}

public struct MineComponent : IEcsIgnoreInFilter
{
    
}

public struct Weapon3Component : IEcsIgnoreInFilter
{
    
}    


