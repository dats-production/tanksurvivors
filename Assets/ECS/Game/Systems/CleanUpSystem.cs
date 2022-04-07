using ECS.Core.Utils.ReactiveSystem;
using ECS.Game.Components.Flags;
using Leopotam.Ecs;

namespace ECS.Game.Systems
{
    public class CleanUpSystem : ReactiveSystem<IsDestroyedComponent>
    {
        protected override EcsFilter<IsDestroyedComponent> ReactiveFilter { get; }
        protected override bool DeleteEvent => false;
        protected override void Execute(EcsEntity entity) => entity.Destroy();
    }
}