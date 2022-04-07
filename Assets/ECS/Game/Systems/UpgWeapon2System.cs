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

public class UpgWeapon2System : ReactiveSystem<UpgWeapon2EventComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, PlayerComponent> _player;
    private EcsFilter<LinkComponent, Weapon2Component> _w2;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<UpgWeapon2EventComponent> ReactiveFilter { get; }    
    
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var level = entity.Get<UpgWeapon2EventComponent>().Level;
        var value = entity.Get<UpgWeapon2EventComponent>().Value;

        switch (level)
        {
            case 1: _w2.GetEntity(0).Get<ProjectileCountAddComponent>().Value += (int)value;
                break;
            case 2: _w2.GetEntity(0).Get<ProjectileSpeedAddComponent>().Value += value;
                break;
            case 3: _w2.GetEntity(0).Get<DamageAddComponent>().Value += value;
                break;
            case 4: _w2.GetEntity(0).Get<ProjectileCountAddComponent>().Value += (int)value;
                break;
            case 5: _w2.GetEntity(0).Get<ExplosionAreaAddComponent>().Value += value;
                break;
            case 6: _w2.GetEntity(0).Get<ProjectileCountAddComponent>().Value += (int)value;
                break;
            case 7: _w2.GetEntity(0).Get<DamageAddComponent>().Value += value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public struct UpgWeapon2EventComponent
{
    public int Level;
    public float Value;
}

