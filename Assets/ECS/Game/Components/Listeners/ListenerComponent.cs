using System;

namespace ECS.Game.Components.Listeners
{
    public struct ListenerComponent<T> 
    {
        public Action<T> Action;
    }
}