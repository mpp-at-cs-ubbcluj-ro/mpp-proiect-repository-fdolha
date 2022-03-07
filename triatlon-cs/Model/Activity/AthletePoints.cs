namespace triatlon_cs.Model.Activity;

// AthletePoints

public class AthletePoints : Entity<int>
{
    
    // Properties

    private int _points;

    public int AthleteId
    {
        get => Id;
    }
    public int Points
    {
        get => _points;
        set => _points = value;
    }
    
    // Lifecycle

    public AthletePoints(int athleteId)
    {
        Id = athleteId;
        _points = 0;
    }

    public AthletePoints(int athleteId, int points)
    {
        Id = athleteId;
        _points = points;
    }

}