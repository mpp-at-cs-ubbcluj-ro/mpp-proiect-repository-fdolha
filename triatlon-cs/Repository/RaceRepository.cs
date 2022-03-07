using triatlon_cs.Model.Activity;

namespace triatlon_cs.Repository;

// RaceRepository

public abstract class RaceRepository : IRepository<int, AthletePoints>
{
    
    // Properties

    private RaceType _raceType;

    public RaceType RaceType
    {
        get => _raceType;
    }
    
    // Lifecycle

    protected RaceRepository(RaceType raceType)
    {
        _raceType = raceType;
    }

    protected RaceRepository(Race race)
    {
        _raceType = race.Type;
    }
    
    // IRepository Methods
    
    public abstract AthletePoints FindOne(int id);
    public abstract IEnumerable<AthletePoints> FindAll();
    public abstract AthletePoints Save(int id);
    public abstract AthletePoints Delete(int id);
    public abstract AthletePoints Update(AthletePoints entity);
    
}