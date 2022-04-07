using ECS.Views;

namespace ECS.Game.Components
{
    public struct LinkComponent
    {
        public ILinkable View;
        
        public T Get<T>() where T : ILinkable
        {
            return (T) View;
        }
    }
}