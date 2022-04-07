using Leopotam.Ecs;
using Services.PauseService;
using UnityEngine;

namespace ECS.Views
{
    public interface ILinkable
    {
        int Hash { get; }
        Transform Transform { get; }
        int UnityInstanceId { get; }
        void Link(EcsEntity entity);
    }
}