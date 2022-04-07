using System;

namespace ECS.Game.Components.Listeners.Impl
{
    public struct IsAvailableListenerComponent : IListener
    {
        public Action<bool> Value;
    }
}