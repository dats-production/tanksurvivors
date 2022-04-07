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

namespace ECS.Views.Impls
{
    public class ExplosionView : LinkableView, IPoolMember
    {
        [Inject] private IGameConfig _config;
        [SerializeField] private GameObject view;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private GameObject[] _allParticles;
        [SerializeField] private GameObject light;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            transform.SetParent(GameObject.Find("[Projectiles]").transform);
        }
        
        public void EnableView(bool added)
        {
            view.SetActive(added);
        }

        public void SetScale(float radius)
        {
            var diametr = radius / 2;            
            foreach (var p in _allParticles)
            {
                p.transform.localScale = new Vector3(diametr, diametr, diametr);
            }
        }

        public void PlayParticles()
        {
            _particle.Play();
            Invoke("DelAndFire",1);
            light.SetActive(true);
            Invoke("DisableLight", 0.1f);
        }

        private void DisableLight()
        {
            light.SetActive(false);
        }

        private void DelAndFire()
        {
            Entity.DelAndFire<IsAvailableComponent>();
        }
    }
}