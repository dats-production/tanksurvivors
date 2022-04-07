using System;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ECS.Views.Impls
{
    public class ExperienceView : LinkableView, IPoolMember, ITrigger
    {
	    [Inject] private IGameConfig _config;
	    [SerializeField] private Transform center;
	    [SerializeField] private GameObject view;
	    
	    public override void Link(EcsEntity entity)
	    {
		    base.Link(entity);
		    transform.SetParent(GameObject.Find("[Experience]").transform);
	    }
	    public void EnableView(bool added)
	    {
		    var exp = Entity.Get<ExperienceComponent>().Value;
		    view.SetActive(added);
		    if(exp <= 50)
			    view.transform.GetChild(0).gameObject.SetActive(added);
		    else if (exp > 50 && exp<100)
			    view.transform.GetChild(1).gameObject.SetActive(added);
		    else
				view.transform.GetChild(2).gameObject.SetActive(added);
	    }
	    public Transform Center => center;

	    public float GetTriggerDistance()
	    {
		    return _config.ExperienceCfg.pickUpRadius;
	    }

	    public void OnDrawGizmos()
	    {
		    // Gizmos.color = Color.magenta;
		    // Gizmos.DrawSphere(Center.position, GetTriggerDistance());
	    }
    }
}