namespace triatlon_cs.Model;

// Entity

public class Entity<TId>
{
    
    // Properties

    private TId? _id;

    protected TId? Id
    {
        get => _id;
        set => _id = value;
    }


}