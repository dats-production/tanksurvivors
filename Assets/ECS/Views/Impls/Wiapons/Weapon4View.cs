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
    public class Weapon4View : LinkableView, IDamager
    {
	    [Inject] private IGameConfig _config;
	    public Transform view;
	    public override void Link(EcsEntity entity)
	    {
		    base.Link(entity);
		    var scale = view.localScale;
		    scale.x = _config.WeaponsCfg.w4.attackRange*2/10;
		    scale.z = _config.WeaponsCfg.w4.attackRange*2/10;
		    view.localScale = scale;
	    }

	    public float GetAttackRange()
	    {
		    return _config.WeaponsCfg.w4.attackRange;
	    }

	    public float Damage => Random.Range(_config.WeaponsCfg.w4.damageMin, _config.WeaponsCfg.w4.damageMax);

	    public void DealDamage(IDamageable damageble, float damageAdd) => damageble.TakeDamage(Damage + damageAdd);

	    public void OnDrawGizmos()
	    {
		    //Gizmos.color = Color.magenta; 
		    //Gizmos.DrawSphere(transform.position, _config.WeaponsCfg.w4.attackRange);
	    }
    }
}
