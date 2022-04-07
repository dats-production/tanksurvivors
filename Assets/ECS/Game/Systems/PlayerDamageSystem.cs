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
using Runtime.Game.Ui;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Scripts.Runtime.Game.Ui.Windows.GameOver;
using Services.Uid;
using SimpleUi.Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerDamageSystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent> _enemies;
    private EcsFilter<LinkComponent, PlayerComponent> _player;
    private EcsFilter<LinkComponent, HealthBarComponent, HealthComponent> _hb;

    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;

    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var playerView = _player.Get1(0).Get<PlayerView>();
        ref var curHealth = ref _hb.Get3(0).Current;
        var maxHealth = _hb.Get3(0).Max;
        var hbView = _hb.Get1(0).Get<HealthBarView>();
        foreach (var e in _enemies)
        {
            var enemyView = _enemies.Get1(e).View as EnemyView;
            if(Vector3.Distance(playerView.Center.position, enemyView.Center.position) 
               < playerView.GetTriggerDistance() + enemyView.GetTriggerDistance()*2
               || Vector3.Distance(playerView.Center2.position, enemyView.Center.position) 
               < playerView.GetTriggerDistance() + enemyView.GetTriggerDistance()*2)
            {
                _enemies.GetEntity(e).Get<StopComponent>();
                enemyView.EnableAttack();
                if(_enemies.GetEntity(e).Has<IsAvailableComponent>())
                {
                    var damage = enemyView.GetDamage();
                    curHealth -= damage * Time.deltaTime;
                    hbView.UpdateHealth(curHealth, maxHealth);
                    playerView.MakeRedColor();
                }
                if (curHealth <= 0)
                {
                    _signalBus.OpenWindow<GameOverWindow>();
                }
            }
            else
                _enemies.GetEntity(e).Del<StopComponent>();
                
        }
    }
}

public struct StopComponent : IEcsIgnoreInFilter
{
    
}


