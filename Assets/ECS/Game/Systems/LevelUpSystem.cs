using System;
using System.Diagnostics.CodeAnalysis;
using DataBase.Game;
using DataBase.Objects;
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
using Scripts.Runtime.Game.Ui.Windows.LevelUp;
using Services.Uid;
using Signals;
using SimpleUi.Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class LevelUpSystem : ReactiveSystem<LevelUpComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, PlayerLevelComponent> _player;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<LevelUpComponent> ReactiveFilter { get; }    
    private UltimateJoystick _ultimateJoystick;
    
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        
        _signalBus.OpenWindow<LevelUpWindow>();
    }
}



public struct LevelUpComponent : IEcsIgnoreInFilter
{
    
}

