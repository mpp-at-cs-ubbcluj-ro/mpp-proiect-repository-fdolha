using System.Collections.Generic;
using Model;
using Model.Activity;
using Model.Person;

namespace Service
{
    public interface ITriatlonService
    {
        Referee LogInReferee(string email, string password, ITriatlonObserver observer);
        void LogOutReferee(string email);
        List<Athlete> GetAthletes();
        List<Result> GetAthletesWithTotalPoints();
        void AddResult(Referee referee, int athleteId, int points);
        List<Result> GetParticipantsWithResultInRace(RaceType raceType);
    }
}