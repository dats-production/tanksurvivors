using ECS.Core.Utils.ReactiveSystem;
using ECS.Core.Utils.ReactiveSystem.Components;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils;
using ECS.Views;
using ECS.Views.Impls;
using Leopotam.Ecs;
using Zenject;

namespace ECS.Game.Systems
{
    public class InstantiateSystem : ReactiveSystem<EventAddComponent<PrefabComponent>>
    {
        [Inject] private readonly ISpawnService<EcsEntity, ILinkable> _spawnService;
        protected override EcsFilter<EventAddComponent<PrefabComponent>> ReactiveFilter { get; }
        protected override void Execute(EcsEntity entity)
        {
            var linkable = _spawnService.Spawn(entity);
            linkable?.Link(entity);
            entity.Get<LinkComponent>().View = linkable;

            var chunkView = linkable as ChunkView;
            if (chunkView)
            {
                var id = entity.Get<ChunkComponent>().id;
                chunkView.SetChunkActive(id);
            }
        }
    }
}