using System;
using DataBase.Objects;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ECS.Views.Impls
{
    public class BulletView : LinkableView, IDamager, IPoolMember, ITrigger
    {
        [Inject] private IGameConfig _config;
        [SerializeField] private GameObject view;
        [Inject] private readonly EcsWorld _world;
        
        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            transform.SetParent(GameObject.Find("[Projectiles]").transform);
        }
        public void EnableView(bool added)
        {
            view.SetActive(added);
        }

        public float Damage => Random.Range(_config.WeaponsCfg.w1.damageMin, _config.WeaponsCfg.w1.damageMax);

        public void DealDamage(IDamageable damageble, float damageAdd) 
            => damageble.TakeDamage(Damage + damageAdd);

        public Transform Center => throw new NotImplementedException();

        public float GetTriggerDistance()
        {
            return _config.WeaponsCfg.w1.bulletRadius;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, GetTriggerDistance());
        }
    }
}