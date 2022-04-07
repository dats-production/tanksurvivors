using System;
using DataBase.Objects;
using ECS.Game.Components.Flags;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;

namespace ECS.Views.Impls
{
    public class Weapon2View : LinkableView
    {
	    [Inject] private IGameConfig _config;
	    public Transform muzzle;
	    
	    public float GetAttackRange()
	    {
		    return _config.WeaponsCfg.w2.attackRange;
	    }
	    // public void OnDrawGizmos()
	    // {
		   //  Debug.Log(GetAttackRange());
		   //  Gizmos.color = Color.blue;
		   //  Gizmos.DrawSphere(Transform.position, GetAttackRange());
	    // }
    }
}
