using DataBase.Game;
using ECS.Core.Utils.SystemInterfaces;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using ECS.Views;
using ECS.Views.Impls;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;

namespace ECS.Game.Systems.Move
{
    public class HomingMissileSystem : IEcsUpdateSystem
    {
        [Inject] private IGameConfig _config;
        private readonly EcsWorld _world;
        private readonly EcsFilter<MissileTargetComponent, IsAvailableComponent> _missile;
        private readonly EcsFilter<LinkComponent, EnemyComponent, IsAvailableComponent> _enemies;
        private readonly EcsFilter<LinkComponent, ExplosionComponent>.Exclude<IsAvailableComponent> _explosions;
        private EcsFilter<Weapon2Component> _w2;

        private Vector3 newDirection;

        public void Run()
        {
            if(_world.GetGameStage().Get<GameStageComponent>().Value != EGameStage.Play) return;
            
            foreach (var m in _missile)
            {
                var speed = _missile.Get1(m).Speed;
                var targetView = _missile.Get1(m).Enemy.Get<LinkComponent>().View as ITrigger;
                var target = targetView.Center;
                var missileView = _missile.GetEntity(m).Get<LinkComponent>().View as MissileView;
                var missileTr = missileView.Transform;
                var missilePos = missileView.Transform.position;
                ref var rotStep = ref _missile.Get1(m).RotationStep;
                rotStep += speed*speed * Time.deltaTime / 5000;
                newDirection = Vector3.RotateTowards(missileTr.forward, target.position - missilePos,
                    rotStep, 10); 
                //Debug.DrawRay(missilePos, newDirection, Color.red);
                missileTr.rotation = Quaternion.LookRotation(newDirection);
                missileTr.position += missileTr.forward * speed * Time.deltaTime;

                if (Vector3.Distance(missilePos, target.position) 
                      > targetView.GetTriggerDistance() + missileView.GetTriggerDistance()) continue;
                var damageAdd = _w2.GetEntity(0).Get<DamageAddComponent>().Value;
                var explosionRadiusAdd = _w2.GetEntity(0).Get<ExplosionAreaAddComponent>().Value;
                _missile.GetEntity(m).Get<ExplosionDamageComponent>().explPos = missilePos;
                var explRadius = missileView.GetExplosionRadius() * (1 + explosionRadiusAdd);
                _missile.GetEntity(m).Get<ExplosionDamageComponent>().explRadius = explRadius;
                _missile.GetEntity(m).Get<ExplosionDamageComponent>().damageAdd = damageAdd;
                _missile.GetEntity(m).Del<MissileTargetComponent>();
                _missile.GetEntity(m).DelAndFire<IsAvailableComponent>();
                foreach (var e in _explosions)
                {
                    var explEntity = _explosions.GetEntity(e);
                    explEntity.GetAndFire<IsAvailableComponent>();
                    explEntity.Get<PositionComponent>().Value = missilePos;
                    var explView = _explosions.Get1(e).Get<ExplosionView>();
                    explView.SetScale(explRadius);
                    explView.PlayParticles();
                    return;
                }
            }
        }
    }

    public struct MissileTargetComponent
    {
        public EcsEntity Enemy;
        public float Speed;
        public float RotationStep;
    }
}