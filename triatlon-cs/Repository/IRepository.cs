using triatlon_cs.Model;

namespace triatlon_cs.Repository;

// IRepository

public interface IRepository<in TId, TE> where TE : Entity<TId>
{

    TE FindOne(TId id);
    IEnumerable<TE> FindAll();
    TE Save(TId id);
    TE Delete(TId id);
    TE Update(TE entity);

}