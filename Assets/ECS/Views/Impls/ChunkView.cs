using System;
using ECS.Game.Components.Flags;
using Ecs.Views.Linkable.Impl;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace ECS.Views.Impls
{
    public class ChunkView : LinkableView
    {
        public Transform chunks;
        public NavMeshSurface surface;

        public override void Link(EcsEntity entity)
        {
            base.Link(entity);
            transform.SetParent(GameObject.Find("[CHUNKS]").transform);
        }
        
        public void SetChunkActive(int id)
        {
            chunks.GetChild(id).gameObject.SetActive(true);
        }
    }
}