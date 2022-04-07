using Leopotam.Ecs;

namespace ECS.Game.Components.Flags
{
    public struct ChunkChangeEventComponent
    {
        public ESide side;
    }
}

public enum ESide
{
    Up,
    Down,
    Right,   
    Left
}