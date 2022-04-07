using DataBase.Game;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Views.Impls;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;

public class PlayerInputSystem : IEcsUpdateSystem, IEcsInitSystem
{
    [Inject] private IGameConfig _gameConfig;
    //[Inject] private IGameStageService _gameStage;
    private EcsFilter<PlayerComponent> _player;
    private EcsWorld _world;

    [Inject] private SignalBus _signalBus;

    private UltimateJoystick _ultimateJoystick;

    private readonly EcsFilter<PlayerComponent, DirectionComponent> _direction;
    private readonly EcsFilter<GameStageComponent> _gameStage;

    private float moveX;
    private float moveY;

    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        SetDirection();
        
        foreach (var d in _direction)
        {
            ref var dir = ref _direction.Get2(d).Value;
            dir.x = moveX;
            dir.y = moveY;
        }
    }

    private void SetDirection()
    {
        moveX= _ultimateJoystick.HorizontalAxis;
        moveY= _ultimateJoystick.VerticalAxis;
    }
    
    public void Init()
    {
        _ultimateJoystick = UltimateJoystick.GetUltimateJoystick("Main");
    }
}