using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components;
using ECS.Game.Components.Events;
using Leopotam.Ecs;

namespace ECS.Game.Systems
{
    public class GameStageSystem : ReactiveSystem<ChangeStageComponent>
    {
        protected override EcsFilter<ChangeStageComponent> ReactiveFilter { get; }
        protected override void Execute(EcsEntity entity)
        {
            ref var stage = ref entity.Get<ChangeStageComponent>().Value;
            entity.Get<GameStageComponent>().Value = stage;
        }
    }
}