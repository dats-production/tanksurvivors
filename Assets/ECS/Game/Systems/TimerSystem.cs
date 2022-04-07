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
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class TimerSystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus;
    
    private EcsFilter<TimerComponent> _timer;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var timer = _timer.Get1(0).Value;
        _signalBus.Fire(new SignalTimer(timer));
        var playerData = _playerData.GetData();   
        if (timer.ToInt() > playerData.Timer.ToInt())
        {
            playerData.Timer = timer;
            _playerData.Save(playerData);
        }
    }
}  


