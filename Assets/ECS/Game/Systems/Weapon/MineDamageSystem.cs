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

public class MineDamageSystem : IEcsUpdateSystem
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent>  _enemies;
    private EcsFilter<LinkComponent, MineComponent, IsAvailableComponent, PositionComponent>   _mines;
    private EcsFilter<Weapon3Component> _w3;
    private readonly EcsFilter<LinkComponent, ExplosionComponent>.Exclude<IsAvailableComponent> _explosions;
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;

    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        foreach (var m in _mines)
        {
            var triggerDistanseAdd = _w3.GetEntity(0).Get<TriggerAreaAddComponent>().Value;
            var damageAdd = _w3.GetEntity(0).Get<DamageAddComponent>().Value;
            var explosionRadiusAdd = _w3.GetEntity(0).Get<ExplosionAreaAddComponent>().Value;
            var mineView = _mines.Get1(m).Get<MineView>();
            var minePos = _mines.Get4(m).Value;
            foreach (var e in _enemies)
            {
                var enemyView = _enemies.Get1(e).View as EnemyView;
                if(Vector3.Distance(minePos, enemyView.Transform.position) 
                   > mineView.GetTriggerDistance()*(1+triggerDistanseAdd)) continue;
                //Debug.Log("Trigger " + mineView.GetTriggerDistance()*(1+triggerDistanseAdd));
                _mines.GetEntity(m).Get<ExplosionDamageComponent>().explPos = _mines.Get4(m).Value;
                var explRadius = mineView.GetExplosionRadius() * (1 + explosionRadiusAdd);
                _mines.GetEntity(m).Get<ExplosionDamageComponent>().explRadius = explRadius;
                _mines.GetEntity(m).Get<ExplosionDamageComponent>().damageAdd = damageAdd;
                _mines.GetEntity(m).DelAndFire<IsAvailableComponent>();
                foreach (var expl in _explosions)
                {
                    var explEntity = _explosions.GetEntity(expl);
                    explEntity.GetAndFire<IsAvailableComponent>();
                    explEntity.Get<PositionComponent>().Value = _mines.Get4(m).Value;
                    var explView = _explosions.Get1(expl).Get<ExplosionView>();
                    explView.SetScale(explRadius);
                    explView.PlayParticles();
                    return;
                }
            }
        }
    }
}


