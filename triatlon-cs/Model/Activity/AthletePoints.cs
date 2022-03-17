namespace triatlon_cs.Model.Activity;

// AthletePoints

public class AthletePoints : Entity<int>
{
    
    // Properties

    private int _athleteId;
    private int _points;

    public int AthleteId
    {
        get => _athleteId;
        set => _athleteId = value;
    }
    public int Points
    {
        get => _points;
        set => _points = value;
    }
    
    // Lifecycle

    public AthletePoints(int athleteId)
    {
        _athleteId = athleteId;
        _points = 0;
    }

    public AthletePoints(int athleteId, int points)
    {
        _athleteId = athleteId;
        _points = points;
    }

}