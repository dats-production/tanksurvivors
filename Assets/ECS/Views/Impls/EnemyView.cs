using System;
using ECS.Game.Components;
using ECS.Game.Components.Flags;
using ECS.Utils.Extensions;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using PdUtils;
using Runtime.DataBase.General.GameCFG;
using Runtime.Game.Ui;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ECS.Views.Impls
{
	public class EnemyView : LinkableView, IDamageable, IPoolMember, ITrigger
	{
		[Inject] private IGameConfig _config;
		[Inject] private readonly EcsWorld _world;
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Transform center;
		[SerializeField] private Transform viewParent;
		private GameObject view;
		public Uid uid;
		
		private float _currentHealth;
		private float _damage;
		private float _speed;
		private float _enemyRadius;
		private int _exp;
		
		public void EnableAttack()
		{
			if(view.TryGetComponent<Animator>(out var animator))
			{
				animator.SetTrigger("Attack");
			}
		}
		
		public void SetAgentActive(bool isOn)
		{
			_agent.enabled = isOn;
		}
		
		public override void Link(EcsEntity entity)
		{
			base.Link(entity);
			transform.SetParent(GameObject.Find("[ENEMIES]").transform);
			uid = entity.Get<UIdComponent>().Value;
		}

		public float GetDamage()
		{
			return _damage;
		}
		public void SetEnemyData(Enemy enemy)
		{
			view = viewParent.GetChild(enemy.enemyType).gameObject;
			_currentHealth = enemy.health;
			_damage = enemy.damage;
			_speed = enemy.moveSpeed;
			_exp = enemy.exp;

			_enemyRadius = enemy.enemyType switch
			{
				0 => 0.75f,
				1 => 0.75f,
				2 => 1f,
				3 => 1f,
				4 => 1f,
				5 => 1f,
				6 => 2,
				7 => 2,
			};
}
	    
	    public void Move(Transform player)
		{
			if (_agent.enabled == false) return;
			if (Entity.Has<StopComponent>())
			{
				_agent.speed = 0;
			}
			else
			{
				_agent.speed = _speed;
				_agent.SetDestination(player.position);
				Invoke("DeletePosComp", 0.1f);				
			}

		}
	    public void EnableView(bool added)
	    {
		    view.SetActive(added);
	    }
	    public Transform Center => center;

	    public void TakeDamage(float damage)
	    {
		    _currentHealth -= damage;
		    if(_currentHealth<=0) Die();
	    }

	    public void Die()
	    {
		    Entity.Get<DeathComponent>().expFromEnemy = _exp;
		    Entity.DelAndFire<IsAvailableComponent>();
	    }

	    public float GetTriggerDistance()
	    {
		    return _enemyRadius;
	    }

	    public void OnDrawGizmos()
	    {
		    Gizmos.color = Color.magenta;
		    Gizmos.DrawSphere(Center.position, GetTriggerDistance());
	    }

	    private void DeletePosComp()
	    {
		    Entity.Del<PositionComponent>();
	    }


    }
}