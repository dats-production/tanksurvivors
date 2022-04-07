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
using Runtime.Game.Ui;
using Runtime.Services.CommonPlayerData;
using Runtime.Services.CommonPlayerData.Data;
using Services.Uid;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class DamageUISystem : ReactiveSystem<DamageUIEventComponent>
{
    [Inject] private readonly ICommonPlayerDataService<CommonPlayerData> _playerData;
    [Inject] private IGameConfig _config;
    [Inject] private SignalBus _signalBus; 
    
    private EcsFilter<DamageUIComponent>.Exclude<IsAvailableComponent> _damageUI;

    private readonly EcsFilter<GameStageComponent> _gameStage;
    private readonly EcsWorld _world;
    protected override EcsFilter<DamageUIEventComponent> ReactiveFilter { get; }    
    
    public int sortingOrder;
    
    protected override void Execute(EcsEntity entity)
    {
        if (_gameStage.Get1(0).Value != EGameStage.Play) return;

        var enemyPos = entity.Get<DamageUIEventComponent>().position;
        var damage = entity.Get<DamageUIEventComponent>().damage;
      
        foreach (var d in _damageUI)
        {
            //Debug.Log(_damageUI.GetEntitiesCount());           
            var damageUI = _damageUI.GetEntity(d);
            damageUI.GetAndFire<IsAvailableComponent>();

            var rndX = Random.Range(-.5f, .5f);
            var rndY = Random.Range(3f, 4f);            
            damageUI.Get<PositionComponent>().Value = enemyPos + new Vector3(rndX, rndY, 0.53f);
            damageUI.Get<RotationComponent>().Value = Quaternion.Euler(new Vector3(60, 0, 0));
            var damageUIView = damageUI.Get<LinkComponent>().Get<DamageUIView>();
            //if(damageUIView)
            {
                damageUIView.SetDamageUI(damage);
                sortingOrder++;
                damageUIView.SetSortingOrder(sortingOrder);
                damageUIView.OnAnimationComplite();
            }
            break;
        }
    }
}

public struct DamageUIComponent
{
    public float Value;
}

public struct DamageUIEventComponent
{
    public float damage;
    public Vector3 position;
}

