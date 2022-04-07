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
    public class Weapon3View : LinkableView
    {
	    [Inject] private IGameConfig _config;
	    public Transform muzzle;
    }
}
