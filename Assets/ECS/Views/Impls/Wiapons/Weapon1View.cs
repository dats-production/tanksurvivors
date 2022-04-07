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
    public class Weapon1View : LinkableView
    {
	    [Inject] private IGameConfig _config;
	    [SerializeField] private GameObject light;
	    public Transform muzzle;
	    
	    public void AimTurretOnTarget(Transform target, float aimSpeedAdd)
	    {
		    Vector3 direction = target.position - transform.position;
		    Quaternion lookRotation = Quaternion.LookRotation(direction);
		    var rotSpeed = Time.deltaTime * _config.WeaponsCfg.w1.rotationSpeed * (1 + aimSpeedAdd);
		    Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotSpeed).eulerAngles;
		    transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	    }
	    public void SetPosition(Vector3 player)
	    {
		    transform.position = player;
	    }
	    public float GetAttackRange()
	    {
		    return _config.WeaponsCfg.w1.attackRange;
	    }

	    public void EnableLight()
	    {
		    light.SetActive(true);
		    Invoke("DisableLight", 0.05f);
	    }

	    private void DisableLight()
	    {
		    light.SetActive(false);
	    }
	    // public void OnDrawGizmos()
	    // {
		   //  Gizmos.color = Color.blue;
		   //  Gizmos.DrawSphere(Transform.position, GetAttackRange());
	    // }
    }
}
