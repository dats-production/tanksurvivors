using System.Diagnostics;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using ECS.Views;
using Ecs.Views.Linkable.Impl;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Runtime.Game.Ui
{
    public class DamageUIView : LinkableView, IPoolMember
    {
        public TMP_Text damageText;
        [SerializeField] private GameObject view;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Animator animator;
        
        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            transform.SetParent(GameObject.Find("[DamageUI]").transform);
        }	 
        public void SetDamageUI(float value)
        {
            damageText.text = "-" + value.ToString("0" );
        }

        public void EnableView(bool added)
        {
            view.SetActive(added);
        }

        public void SetSortingOrder(int value)
        {
            canvas.sortingOrder = value;
        }
        
        public void OnAnimationComplite()
        {
            Invoke("DelAndFire",2);
        }

        private void DelAndFire()
        {
            Entity.DelAndFire<IsAvailableComponent>();
        }
    }
}
