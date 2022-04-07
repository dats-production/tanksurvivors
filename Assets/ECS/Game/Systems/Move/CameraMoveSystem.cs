using System;
using DataBase.Game;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using ECS.Views.Impls;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class CameraMoveSystem : IEcsUpdateSystem
{
    [Inject] private IGameConfig _gameConfig;
    //[Inject] private IGameStageService _gameStage;
    private EcsFilter<LinkComponent, CameraComponent> _camera;
    private EcsFilter<LinkComponent, PlayerComponent, DirectionComponent>  _player;
    private readonly EcsFilter<GameStageComponent> _gameStage;

    [Inject] private SignalBus _signalBus;
    
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        
        var cameraView = _camera.Get1(0).View as CameraView;

        var playerTransform = _player.Get1(0).View.Transform;

        if (cameraView != null) cameraView.Move(playerTransform);
    }
}