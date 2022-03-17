using triatlon_cs.Model;

namespace triatlon_cs.Repository;

// IRepository

public interface IRepository<TId, TE> where TE : Entity<TId>
{

    TE FindOne(TId id);
    IEnumerable<TE> FindAll();
    TE Save(TE entity);
    TId Delete(TId id);
    TE Update(TE entity);

}