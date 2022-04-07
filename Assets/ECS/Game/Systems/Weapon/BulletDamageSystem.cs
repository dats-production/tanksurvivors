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
using Services.Uid;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class BulletDamageSystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent> _enemies;
    private EcsFilter<LinkComponent, BulletComponent, IsAvailableComponent> _bullets;
    private EcsFilter<Weapon1Component> _w1;
    private EcsFilter<LinkComponent, PlayerComponent> _player;
    private EcsFilter<LinkComponent, SparksComponent>.Exclude<IsAvailableComponent> _sparks;

    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;

    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;
        
        foreach (var b in _bullets)
        {
            var bulletView = _bullets.Get1(b).Get<BulletView>();
            foreach (var e in _enemies)
            {
                var enemyView = _enemies.Get1(e).View as EnemyView;
                if(Vector3.Distance(bulletView.Transform.position, enemyView.Center.position) 
                   > enemyView.GetTriggerDistance()+ bulletView.GetTriggerDistance()) continue;

                if (!enemyView) continue;
                if (_bullets.GetEntity(b).Has<IsAvailableComponent>())
                {
                    var hitPos = bulletView.Transform.position;
                    var playerPos = _player.Get1(0).View.Transform.position;
                    foreach (var s in _sparks)
                    {
                        _sparks.GetEntity(s).Get<PositionComponent>().Value = hitPos;
                        _sparks.GetEntity(s).GetAndFire<IsAvailableComponent>();
                        var sparksView = _sparks.Get1(s).Get<SparksView>();
                        sparksView.PlayParticles();
                        break;
                    }                    
                }
                var damageAdd = _w1.GetEntity(0).Get<DamageAddComponent>().Value;
                _enemies.GetEntity(e).Get<DamageUIEventComponent>().position = enemyView.Transform.position;
                _enemies.GetEntity(e).Get<DamageUIEventComponent>().damage = bulletView.Damage + damageAdd;
                bulletView.DealDamage(enemyView, damageAdd);
                _bullets.GetEntity(b).DelAndFire<IsAvailableComponent>();
            }
        }
    }
}


