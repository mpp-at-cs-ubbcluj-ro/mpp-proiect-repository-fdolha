using System.Collections.Generic;
using Triatlon.Proto;

namespace Service
{
    public interface ITriatlonService
    {
        Referee LogInReferee(string email, string password, ITriatlonObserver observer);
        void LogOutReferee(string email);
        List<Athlete> GetAthletes();
        List<Result> GetAthletesWithTotalPoints();
        void AddResult(Triatlon.Proto.Referee referee, int athleteId, int points);
        List<Result> GetParticipantsWithResultInRace(Triatlon.Proto.RaceType raceType);
    }
}