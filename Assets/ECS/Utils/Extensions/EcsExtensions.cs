using DataBase.Game;
using ECS.Game.Components;
using ECS.Game.Components.Events;
using ECS.Game.Components.Flags;
using ECS.Game.Components.Input;
using Leopotam.Ecs;
using PdUtils;

namespace ECS.Utils.Extensions
{
    public static class EcsExtensions
    {
        public static EcsEntity GetEntity<T>(this EcsWorld world) where T : struct
        {
            var filter = world.GetFilter(typeof(EcsFilter<T>));
            foreach (var i in filter)
                return filter.GetEntity(i);
            return default;
        }

        public static void SetStage(this EcsWorld world, EGameStage value) => world.GetGameStage().Get<ChangeStageComponent>().Value = value;

        public static EcsEntity GetGameStage(this EcsWorld world)
        {
            var filter = world.GetFilter(typeof(EcsFilter<GameStageComponent>));
            return filter.GetEntity(0);
        }


        public static EcsEntity GetEntityWithUid(this EcsWorld world, Uid uid)
        {
            var value = new EcsEntity();
            var filter = world.GetFilter(typeof(EcsFilter<UIdComponent>));
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                if (uid.Equals(entity.Get<UIdComponent>().Value))
                    return entity;
            }
            return value;
        }

        public static void DeclareOneFrameEvents(this EcsSystems systems)
        {
            systems.OneFrame<TimerTickEventComponent>();
            systems.OneFrame<PointerUpComponent>();
            systems.OneFrame<PointerDragComponent>();
            systems.OneFrame<PointerDownComponent>();
        }
    }
}