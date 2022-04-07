using System;
using DataBase.Shop.Impl;
using DG.Tweening;
using ECS.Game.Components.Flags;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using Zenject;

namespace ECS.Views.Impls
{
    public class PlayerView : LinkableView, ITrigger
    {
	    [SerializeField] private Material tankMat;
        [SerializeField] private CharacterController _controller;
        [Inject] private IGameConfig _config;
        [SerializeField] private Transform center;
        [SerializeField] private Transform center2;
        [SerializeField] private Transform weapon1Tr;
        [SerializeField] private GameObject[] weapons;

        [Range(0.0f, 0.3f)] 
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;

        private float _speed;
        private float _targetRotation;
        private float _rotationVelocity;
        private bool isSprint;

        public override void Link(EcsEntity entity)
        {
	        base.Link(entity);
	        tankMat.color = Color.white;
        }

        public void MakeRedColor()
        {
	        tankMat.color = new Color(1,0.5f,0.5f);
	        
	        Invoke("MakeNormalColor", 0.5f);
        }
        public void MakeNormalColor()
        {
			tankMat.color = Color.white;
        }
        
        public Vector3 GetWeapon1Pos()
        {
	        return weapon1Tr.position;
        }
        
        public void ActivateWeaponView(EUpgradeType type)
        {
	        switch (type)
	        {
		        case EUpgradeType.Weapon2:
			        weapons[1].SetActive(true);
			        break;
		        case EUpgradeType.Weapon3:
			        weapons[2].SetActive(true);
			        break;
		        case EUpgradeType.Weapon4:
			        weapons[3].SetActive(true);
			        break;
		        default:
			        throw new ArgumentOutOfRangeException(nameof(type), type, null);
	        }
        }
        
	    public void Move(Vector2 move, float speed)
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = isSprint ? _config.PlayerCfg.sprintSpeed : speed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			//float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}
			// normalise input direction
			Vector3 inputDirection = new Vector3(move.x, 0.0f, move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (move != Vector2.zero)
			{
				_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

				// rotate to face input direction relative to camera position
				transform.rotation = Quaternion.Euler(0, rotation, 0);
			}

			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

			// move the player
			_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime));
		}

	    public Transform Center => center;
	    public Transform Center2 => center2;

	    public float GetTriggerDistance()
	    {
		    return _config.PlayerCfg.triggerDistance;
	    }

	    public void OnDrawGizmos()
	    {
		    Gizmos.color = Color.white;
		    Gizmos.DrawSphere(Center.position, _config.PlayerCfg.triggerDistance);
		    Gizmos.DrawSphere(Center2.position, _config.PlayerCfg.triggerDistance);
	    }
    }
}