namespace ECS.Utils
{
    public interface ISpawnService<in TEntity, out TObject>
    {
        TObject Spawn(TEntity entity);
    }
}