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

public class ActivateWeaponSystem : ReactiveSystem<ActivateWeaponEventComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, PlayerComponent> _player;
    private EcsFilter<LinkComponent, Weapon2> _w2;
    private EcsFilter<LinkComponent, Weapon3> _w3;
    private EcsFilter<LinkComponent, Weapon4> _w4;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<ActivateWeaponEventComponent> ReactiveFilter { get; }    
    
    protected override void Execute(EcsEntity entity)
    {
        //if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var type = entity.Get<ActivateWeaponEventComponent>().Type;

        var playerView = _player.Get1(0).Get<PlayerView>();
        playerView.ActivateWeaponView(type);
        
        switch (type)
        {
            case EUpgradeType.Weapon2: ActivateWeapon<Weapon2View, Weapon2Component>();
                break;
            case EUpgradeType.Weapon3: ActivateWeapon<Weapon3View, Weapon3Component>();
                break;
            case EUpgradeType.Weapon4: ActivateWeapon<Weapon4View, Weapon4Component>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ActivateWeapon<V, C>() where V : Object where C : struct
    {
        var wFromScene = Object.FindObjectsOfType<V>();
        foreach (var view in wFromScene)
        {
            var link = view as ILinkable;
            var entity = _world.NewEntity();
            entity.Get<C>();
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            link.Link(entity);
            entity.Get<LinkComponent>().View = link;
        }
    }
}

public struct ActivateWeaponEventComponent
{
    public EUpgradeType Type;
}

