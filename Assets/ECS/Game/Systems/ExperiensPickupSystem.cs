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
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ExperiensPickupSystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, ExperienceComponent, IsAvailableComponent>  _exps;
    private EcsFilter<LinkComponent, PlayerComponent, TriggerPickupComponent>   _player;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;

    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        foreach (var p in _player)
        {
            var playerView = _player.Get1(p).Get<PlayerView>();
            var triggerPickupDistanse = _player.Get3(p).Value;
            foreach (var e in _exps)
            {
                var expView = _exps.Get1(e).View as ExperienceView;
                if(Vector3.Distance(playerView.Center.position, expView.Center.position) 
                   > expView.GetTriggerDistance() + triggerPickupDistanse
                   || Vector3.Distance(playerView.Center2.position, expView.Center.position) 
                   > expView.GetTriggerDistance() + triggerPickupDistanse) continue;
                _player.GetEntity(p).Get<AddExpEventComponent>().Value = _exps.Get2(e).Value;
                _exps.GetEntity(e).DelAndFire<IsAvailableComponent>();
            }
        }
    }
}
public struct ExperienceComponent
{
    public int Value;
}


