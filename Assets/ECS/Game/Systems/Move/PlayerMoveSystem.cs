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
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using Runtime.Game.Ui;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerMoveSystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private readonly PlayerConfigSettings _playerConfigSettings;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, PlayerComponent, DirectionComponent, SpeedComponent>  _player;
    private EcsFilter<LinkComponent, HealthBarComponent> _hb;
    private EcsFilter<LinkComponent, Weapon1Component> _w1;
    private readonly EcsFilter<GameStageComponent> _gameStage;

    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        
        foreach (var p in _player)
        {
            
            var playerView = _player.Get1(p).View as PlayerView;
            var dir = _player.Get3(p).Value;
            var speed = _player.Get4(p).Value;
            playerView.Move(dir, speed);
            var hbView = _hb.Get1(0).Get<HealthBarView>();
            hbView.SetPosition(playerView.Transform.position);
            if(_w1.IsEmpty()) return;
            var w1View = _w1.Get1(0).Get<Weapon1View>();
            w1View.SetPosition(playerView.GetWeapon1Pos());
        }
    }
}