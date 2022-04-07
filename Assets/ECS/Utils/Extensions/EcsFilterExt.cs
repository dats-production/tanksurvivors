using ECS.Game.Components;
using Leopotam.Ecs;
using UnityEngine;

public static class EcsFilterExt
{
    public static bool TryGetLinkOf(this EcsFilter<LinkComponent> filter, GameObject go, out EcsEntity entity)
    {
        foreach (int i in filter)
        {
            if (filter.Get1(i).View.UnityInstanceId == go.GetInstanceID())
            {
                entity = filter.GetEntity(i);
                return true;
            }
        }
        entity = EcsEntity.Null;
        return false;
    }
    
    public static bool TryGetLinkOf<T>(this EcsFilter<LinkComponent>.Exclude<T> filter, GameObject go, out EcsEntity entity)
        where T : struct
    {
        foreach (int i in filter)
        {
            if (filter.Get1(i).View.UnityInstanceId == go.GetInstanceID())
            {
                entity = filter.GetEntity(i);
                return true;
            }
        }
        entity = EcsEntity.Null;
        return false;
    }
    
    public static bool TryGetLinkOf<T>(this EcsFilter<LinkComponent, T> filter, GameObject go, out EcsEntity entity)  
        where T : struct
    {
        foreach (int i in filter)
        {
            if (filter.Get1(i).View.UnityInstanceId == go.GetInstanceID())
            {
                entity = filter.GetEntity(i);
                return true;
            }
        }
        entity = EcsEntity.Null;
        return false;
    }
    public static bool TryGetLinkOf<T, T2>(this EcsFilter<LinkComponent, T>.Exclude<T2> filter, GameObject go, out EcsEntity entity)  
        where T : struct
        where T2 : struct
    {
        foreach (int i in filter)
        {
            if (filter.Get1(i).View.UnityInstanceId == go.GetInstanceID())
            {
                entity = filter.GetEntity(i);
                return true;
            }
        }
        entity = EcsEntity.Null;
        return false;
    }

    public static bool TryGetLinkOf<T, T2>(this EcsFilter<LinkComponent, T, T2> filter, GameObject go, out EcsEntity entity)  
        where T : struct 
        where T2 : struct
    {
        foreach (int i in filter)
        {
            if (filter.Get1(i).View.UnityInstanceId == go.GetInstanceID())
            {
                entity = filter.GetEntity(i);
                return true;
            }
        }
        entity = EcsEntity.Null;
        return false;
    }
    
    public static bool TryGetLinkOf<T, T2, T3>(this EcsFilter<LinkComponent, T, T2, T3> filter, GameObject go, out EcsEntity entity)  
        where T : struct 
        where T2 : struct 
        where T3 : struct
    {
        foreach (int i in filter)
        {
            if (filter.Get1(i).View.UnityInstanceId == go.GetInstanceID())
            {
                entity = filter.GetEntity(i);
                return true;
            }
        }
        entity = EcsEntity.Null;
        return false;
    }
    
    public static bool TryGetParentOf(this EcsFilter<UIdComponent> filter, EcsEntity childEntity, out EcsEntity parent)
    {
        if (!childEntity.Has<OwnerComponent>())
        {
            parent = EcsEntity.Null;
            return false;
        }
            
        foreach (int i in filter)
        {
            if (filter.Get1(i).Value == childEntity.Get<OwnerComponent>().Value)
            {
                parent = filter.GetEntity(i);
                return true;
            }
        }

        parent = EcsEntity.Null;
        return false;
    }
        
    public static bool TryGetParentOf<T>(this EcsFilter<UIdComponent, T> filter, EcsEntity childEntity, out EcsEntity parent)
        where T : struct
    {
        if (!childEntity.Has<OwnerComponent>())
        {
            parent = EcsEntity.Null;
            return false;
        }
            
        foreach (int i in filter)
        {
            if (filter.Get1(i).Value == childEntity.Get<OwnerComponent>().Value)
            {
                parent = filter.GetEntity(i);
                return true;
            }
        }

        parent = EcsEntity.Null;
        return false;
    }
        
    public static bool TryGetParentOf<T, T2>(this EcsFilter<UIdComponent, T, T2> filter, EcsEntity childEntity, out EcsEntity parent)
        where T : struct
        where T2 : struct
    {
        if (!childEntity.Has<OwnerComponent>())
        {
            parent = EcsEntity.Null;
            return false;
        }
            
        foreach (int i in filter)
        {
            if (filter.Get1(i).Value == childEntity.Get<OwnerComponent>().Value)
            {
                parent = filter.GetEntity(i);
                return true;
            }
        }

        parent = EcsEntity.Null;
        return false;
    }
        
    public static bool TryGetParentOf<T, T2, T3>(this EcsFilter<UIdComponent, T, T2, T3> filter, EcsEntity childEntity, out EcsEntity parent)
        where T : struct
        where T2 : struct
        where T3 : struct
    {
        if (!childEntity.Has<OwnerComponent>())
        {
            parent = EcsEntity.Null;
            return false;
        }
            
        foreach (int i in filter)
        {
            if (filter.Get1(i).Value == childEntity.Get<OwnerComponent>().Value)
            {
                parent = filter.GetEntity(i);
                return true;
            }
        }
            
        parent = EcsEntity.Null;
        return false;
    }
}