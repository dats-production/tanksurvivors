using System;
using System.Collections.Generic;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Utils.Extensions;
using Leopotam.Ecs;
using Zenject;

namespace ECS
{
    public class EcsMainBootstrap : IInitializable, ITickable, IDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _initUpdateSystems;
        public EcsMainBootstrap(EcsWorld world,
            IList<IEcsUpdateSystem> updateSystems,
            IList<IEcsInitSystem> initSystems)
        {
            _world = world;
            _initUpdateSystems = new EcsSystems(_world);

            if (initSystems.Count > 0)
                _initUpdateSystems.AddRange(initSystems);
            
            if (updateSystems.Count > 0)
                _initUpdateSystems.AddRange(updateSystems);
            
            _initUpdateSystems.DeclareOneFrameEvents();
        }

        public void Initialize()
        {
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_initUpdateSystems);
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
#endif
            //Create filters for use out of systems
            _world.GetFilter(typeof(EcsFilter<UIdComponent>));
            _world.GetFilter(typeof(EcsFilter<Weapon1Component>));
            _world.GetFilter(typeof(EcsFilter<Weapon2Component>));
            _world.GetFilter(typeof(EcsFilter<Weapon3Component>));
            _world.GetFilter(typeof(EcsFilter<Weapon4Component>));
            _world.GetFilter(typeof(EcsFilter<HealthBarComponent>));
            
            _initUpdateSystems?.Init();
        }

        public void Tick()
        {
            _initUpdateSystems?.Run();
        }

        public void Dispose()
        {
            _initUpdateSystems?.Destroy();
            _world?.Destroy();
        }
    }
}
