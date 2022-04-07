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
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AddPoolMembersSystem : IEcsUpdateSystem
{
    [Inject] private readonly GetPointFromScene _getPointFromScene;
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus;
    
    private EcsFilter<DamageUIComponent>.Exclude<IsAvailableComponent> _damageUI;
    private EcsFilter<ExperienceComponent>.Exclude<IsAvailableComponent>  _exps;
    private EcsFilter<MineComponent>.Exclude<IsAvailableComponent>  _mines;
    private EcsFilter<MissileComponent>.Exclude<IsAvailableComponent>  _missiles;
    private EcsFilter<BulletComponent>.Exclude<IsAvailableComponent>  _bullets;
    private EcsFilter<EnemyComponent>.Exclude<IsAvailableComponent>  _enemies;
    private EcsFilter<ExplosionComponent>.Exclude<IsAvailableComponent>  _explosion;
    private EcsFilter<SparksComponent>.Exclude<IsAvailableComponent>  _sparks;
    
    
    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    
    int addNum = 50;
    public void Run()
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        if (_damageUI.GetEntitiesCount() < addNum*4) AddPoolMember<DamageUIComponent>("DamageUI");
        if (_exps.GetEntitiesCount() < addNum) AddPoolMember<ExperienceComponent>("Experience");
        if (_mines.GetEntitiesCount() < addNum) AddPoolMember<MineComponent>("Mine");
        if (_missiles.GetEntitiesCount() < addNum) AddPoolMember<MissileComponent>("Rocket");
        if (_bullets.GetEntitiesCount() < addNum) AddPoolMember<BulletComponent>("Bullet");
        if (_enemies.GetEntitiesCount() < addNum) AddPoolMember<EnemyComponent>("Enemy");
        if (_explosion.GetEntitiesCount() < addNum) AddPoolMember<ExplosionComponent>("Explosion");
        if (_sparks.GetEntitiesCount() < addNum) AddPoolMember<SparksComponent>("Sparks");
        
    }

    private void AddPoolMember<T>(string name) where T : struct
    {
        //for (int i = 0; i < addNum; i++)
        {
            var entity = _world.NewEntity();
            entity.Get<T>();
            entity.Get<PositionComponent>().Value = _config.GlobalCfg.poolPos;
            entity.Get<UIdComponent>().Value = UidGenerator.Next();
            entity.GetAndFire<PrefabComponent>().Value = name;            
        }
    }
}  


