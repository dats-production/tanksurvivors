using System;
using DataBase.Objects;
using ECS.Game.Components.Flags;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ECS.Views.Impls
{
    public class MissileView : LinkableView, IDamager, IPoolMember, ITrigger
    {
        [Inject] private IGameConfig _config;
        [SerializeField] private GameObject view;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            transform.SetParent(GameObject.Find("[Projectiles]").transform);
        }
        
        public void EnableView(bool added)
        {
            view.SetActive(added);
        }

        public float Damage => Random.Range(_config.WeaponsCfg.w2.damageMin, _config.WeaponsCfg.w2.damageMax);

        public void DealDamage(IDamageable damageble, float damageAdd) => damageble.TakeDamage(Damage + damageAdd);

        public float GetExplosionRadius()
        {            
            return _config.WeaponsCfg.w2.explosionRadius;
        }

        // public void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawSphere(transform.position, GetExplosionRadius());
        // }
        public Transform Center => throw new NotImplementedException();

        public float GetTriggerDistance()
        {
            return _config.WeaponsCfg.w2.missileRadius;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, GetTriggerDistance());
        }
    }
}