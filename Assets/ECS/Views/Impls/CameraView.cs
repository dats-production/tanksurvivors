using ECS.Game.Components.Flags;
using Ecs.Views.Linkable.Impl;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Views.Impls
{
    [RequireComponent(typeof(Camera))]
    public class CameraView : LinkableView
    {
        [SerializeField] private Vector3 _posOffset = new Vector3(0, 40, -20);
        //private readonly Vector3 _rotOffset = new Vector3(10, 0, 0);
        public void Move(Transform player)
        {
            transform.position = player.position + _posOffset;
            //transform.eulerAngles = player.eulerAngles + _rotOffset;
        }
    }
}