using DataBase.Objects;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;

namespace ECS.Views.Impls
{
    public class SparksView : LinkableView, IPoolMember
    {
        [Inject] private IGameConfig _config;
        [SerializeField] private GameObject view;
        [SerializeField] private ParticleSystem _particle;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            transform.SetParent(GameObject.Find("[Projectiles]").transform);
        }
        
        public void EnableView(bool added)
        {
            view.SetActive(added);
        }

        public void PlayParticles()
        {
            transform.rotation = Random.rotation;
            _particle.Play();
            Invoke("DelAndFire",1);
        }

        private void DelAndFire()
        {
            Entity.DelAndFire<IsAvailableComponent>();
        }
    }
}