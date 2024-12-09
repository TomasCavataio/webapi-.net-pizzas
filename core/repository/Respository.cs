using webapi.core.entitybase;

namespace webapi.core.repository;

public interface IDatabase<T> where T : EntityBase
{
    protected ICollection<T> Data { get; }
}
public interface IAdd<T> : IDatabase<T> where T : EntityBase
{
    void Add(T entity)
    {
        Data.Add(entity);
    }
}
public interface IGet<T, ID> : IDatabase<T> where T : EntityBase
{
    T Get(ID id)
    {
        var entity = Data.Where(x => x.Id.Equals(id)).FirstOrDefault();
        if (entity == null)
        {
            throw new Exception("");
        }
        return entity;
    }
}
public interface IUpdate<T, ID> : IGet<T, ID> where T : EntityBase
{
    void Update(T entity)
    {
        Data.Remove(entity);
        Data.Add(entity);
    }
}
public interface IRemove<T, ID> : IGet<T, ID> where T : EntityBase
{
    void Remove(T entity)
    {
        Data.Remove(entity);
    }
}

public interface IRepository<T, ID> : IAdd<T>, IUpdate<T, ID>, IRemove<T, ID> where T : EntityBase
{

}

