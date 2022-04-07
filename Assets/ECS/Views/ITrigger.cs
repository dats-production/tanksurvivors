using System;
using ECS.Game.Components.Flags;
using Ecs.Views.Linkable.Impl;
using Game.Utils.MonoBehUtils;
using Leopotam.Ecs;
using Runtime.DataBase.General.GameCFG;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ECS.Views
{
	public interface ITrigger
	{
		Transform Center{ get; }
		float GetTriggerDistance();
		void OnDrawGizmos();
	}
}