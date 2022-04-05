
// Race

using System.Collections.Generic;
using System.Linq;

public struct Race
{
    
    // Properties

    private RaceType _type;
    private List<AthletePoints> _athletePoints;

    public RaceType Type
    {
        get => _type;
    }

    public List<AthletePoints> AthletePoints
    {
        get => _athletePoints;
    }
    
    // Lifecycle

    public Race(RaceType type)
    {
        _type = type;
        _athletePoints = new List<AthletePoints>();
    }
    
    // Private Methods

    private AthletePoints GetAthletePoints(int athleteId)
    {
        return _athletePoints.FirstOrDefault(e => e.AthleteId == athleteId);
    }

    private bool AthletesPointsContainsAthlete(int athleteId)
    {
        return GetAthletePoints(athleteId) != null;
    }
    
    // Instance Methods

    public void AddAthlete(int athleteId)
    {
        if (AthletesPointsContainsAthlete(athleteId))
        {
            return;
        }
        _athletePoints.Add(new AthletePoints(athleteId));
    }

    public void AddAthlete(int athleteId, int points)
    {
        if (AthletesPointsContainsAthlete(athleteId))
        {
            SetPointsForAthlete(athleteId, points);
            return;
        }
        _athletePoints.Add(new AthletePoints(athleteId, points));
    }

    public void SetPointsForAthlete(int athleteId, int points)
    {
        AthletePoints athletePoints = GetAthletePoints(athleteId);
        if (athletePoints != null)
        {
            athletePoints.Points = points;
        }
    }

    public int? GetPointsForAthlete(int athleteId)
    {
        AthletePoints athletePoints = GetAthletePoints(athleteId);
        return athletePoints?.Points ?? null;
    }
    
}