using System;
using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Game.Components.Listeners;
using ECS.Game.Components.Listeners.Impl;
using ECS.Views;
using ECS.Views.Impls;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems.Linked
{
    public class IsAvailableSetViewSystem : ReactiveSystem<EventAddComponent<IsAvailableComponent>, EventRemoveComponent<IsAvailableComponent>>
    {
        [Inject] private IGameConfig _config;
        protected override EcsFilter<EventAddComponent<IsAvailableComponent>> ReactiveFilter { get; }
        protected override EcsFilter<EventRemoveComponent<IsAvailableComponent>> ReactiveFilter2 { get; }
        protected override void Execute(EcsEntity entity, bool added)
        {
            //entity.Get<IsAvailableListenerComponent>().Value?.Invoke(added);
            var view = entity.Get<LinkComponent>().View as IPoolMember;
            view.EnableView(added);
            if (view is EnemyView)
            {
                var enemyView = view as EnemyView;
                enemyView.SetAgentActive(added);
            }
        }
    }
}