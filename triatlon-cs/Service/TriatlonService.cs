
using System;
using System.Collections.Generic;
using System.Linq;

public class TriatlonService
{
    
    // Properties

    private RaceRepository _raceRepository;
    private IRefereeRepository _refereeRepository;
    private IAthleteRepository _athleteRepository;
    private Referee _loggedInReferee = null;
    
    // Lifecycle

    public TriatlonService(RaceRepository raceRepository, IRefereeRepository refereeRepository,
        IAthleteRepository athleteRepository)
    {
        _raceRepository = raceRepository;
        _refereeRepository = refereeRepository;
        _athleteRepository = athleteRepository;
    }
    
    // Instance Methods

    public bool LogInReferee(string email, string password)
    {
        Referee referee = _refereeRepository.FindByEmail(email);
        if (referee == null) return false;
        var isLoggedIn = referee.Email.Equals(email) && referee.Password.Equals(password);
        if (isLoggedIn)
        {
            _loggedInReferee = referee;
            _raceRepository.RaceType = referee.RaceType;
        }
        return isLoggedIn;
    }

    public void LogOutReferee()
    {
        _loggedInReferee = null;
    }

    public Referee CurrentReferee
    {
        get => _loggedInReferee;
    }

    public List<Athlete> GetAthletes()
    {
        List<Athlete> athletes = new List<Athlete>();
        foreach (var athlete in _athleteRepository.FindAll())
        {
            athletes.Add(athlete);
        }
        return athletes;
    }

    public List<Result> GetAthletesWithTotalPoints()
    {
        List<Result> athletesWithResults = new List<Result>();
        foreach (var athlete in _athleteRepository.FindAll())
        {
            var points = 0;
            foreach (var value in Enum.GetValues(typeof(RaceType)))
            {
                _raceRepository.RaceType = (RaceType) value;
                AthletePoints athletePoints = _raceRepository.FindByAthleteId(athlete.Id);
                if (athletePoints != null) points += athletePoints.Points;
            }

            Result athleteResult = new Result(athlete.Id, athlete.FullName, athlete.FullNameReversed, points);
            athletesWithResults.Add(athleteResult);
        }
        _raceRepository.RaceType = _loggedInReferee.RaceType;
        return athletesWithResults.OrderBy(r=> r.ReversedName).ToList();
    }

    public void AddResult(int athleteId, int points)
    {
        AthletePoints athletePoints = _raceRepository.FindByAthleteId(athleteId);
        if (athletePoints == null)
        {
            athletePoints = new AthletePoints(athleteId, points);
            _raceRepository.Save(athletePoints);
        }
        else
        {
            athletePoints.Points = points;
            _raceRepository.Update(athletePoints);
        }
    }

    public List<Result> GetParticipantsWithResultInRace()
    {
        List<Result> results = new List<Result>();
        foreach (var athletePoints in _raceRepository.FindAllWithPoints())
        {
            Athlete athlete = _athleteRepository.FindOne(athletePoints.AthleteId);
            Result result = new Result(athlete.Id, athlete.FullName, athlete.FullNameReversed, athletePoints.Points);
            results.Add(result);
        }
        return results.OrderByDescending(r=> r.Points).ToList();
    }

}