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
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyMoveSystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private readonly PlayerConfigSettings _playerConfigSettings;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, PlayerComponent>  _player;
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent>  _enemies;
    private readonly EcsFilter<GameStageComponent> _gameStage;

    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        foreach (var p in _player)
        {
            var player = _player.Get1(p).View.Transform;

            foreach (var e in _enemies)
            {
                var enemyView = _enemies.Get1(e).View as EnemyView;
                enemyView.Move(player);
            }
        }
    }
}