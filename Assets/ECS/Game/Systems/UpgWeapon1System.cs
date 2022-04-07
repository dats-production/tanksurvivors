using System;
using System.Diagnostics.CodeAnalysis;
using DataBase.Game;
using DataBase.Objects;
using DataBase.Shop.Impl;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
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
using Object = UnityEngine.Object;

public class UpgWeapon1System : ReactiveSystem<UpgWeapon1EventComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, PlayerComponent> _player;
    private EcsFilter<LinkComponent, Weapon1Component> _w1;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<UpgWeapon1EventComponent> ReactiveFilter { get; }    
    
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var level = entity.Get<UpgWeapon1EventComponent>().Level;
        var value = entity.Get<UpgWeapon1EventComponent>().Value;

        switch (level)
        {
            case 1: _w1.GetEntity(0).Get<AimSpeedAddComponent>().Value += value;
                break;
            case 2: _w1.GetEntity(0).Get<DamageAddComponent>().Value += value;
                break;
            case 3: _w1.GetEntity(0).Get<ProjectileCountAddComponent>().Value += (int)value;
                break;
            case 4: _w1.GetEntity(0).Get<CooldownSubComponent>().Value += value;
                break;
            case 5: _w1.GetEntity(0).Get<ProjectileSpeedAddComponent>().Value += value;
                break;
            case 6: _w1.GetEntity(0).Get<DamageAddComponent>().Value += value;
                break;
            case 7: _w1.GetEntity(0).Get<ProjectileCountAddComponent>().Value += (int)value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public struct UpgWeapon1EventComponent
{
    public int Level;
    public float Value;
} 
public struct CooldownSubComponent
{
    public float Value;
} 
public struct AimSpeedAddComponent
{
    public float Value;
} 
public struct DamageAddComponent
{
    public float Value;
} 
public struct ProjectileCountAddComponent
{
    public int Value;
} 
public struct ProjectileSpeedAddComponent
{
    public float Value;
} 
public struct ExplosionAreaAddComponent
{
    public float Value;
} 
public struct TriggerAreaAddComponent
{
    public float Value;
}

