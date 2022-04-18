using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Model.Activity;
using Model.Person;
using Repository;
using Repository.DB;
using Service;

namespace Server
{
    public class TriatlonService : ITriatlonService
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IRefereeRepository _refereeRepository;
        private readonly RaceRepository _raceRepository;
        private readonly IDictionary<string, ITriatlonObserver> _observers;

        public TriatlonService(IDictionary<string, string> properties)
        {
            _athleteRepository = new AthleteDbRepository(properties);
            _refereeRepository = new RefereeDbRepository(properties);
            _raceRepository = new RaceDbRepository(properties, RaceType.Swimming);
            _observers = new Dictionary<string, ITriatlonObserver>();
        }
        
        public Referee LogInReferee(string email, string password, ITriatlonObserver observer)
        {
            var referee = _refereeRepository.FindByEmail(email);
            if (referee == null) return null;
            var isLoggedIn = referee.Email.Equals(email) && referee.Password.Equals(password);
            if (isLoggedIn)
            {
                _observers[referee.Email] = observer;
            }

            return referee;
        }

        public void LogOutReferee(string email)
        {
            _observers.Remove(email);
        }

        public List<Athlete> GetAthletes()
        {
            return _athleteRepository.FindAll().ToList();
        }

        public List<Result> GetAthletesWithTotalPoints()
        {
            var athletesWithResults = new List<Result>();
            foreach (var athlete in _athleteRepository.FindAll())
            {
                var points = 0;
                foreach (var value in Enum.GetValues(typeof(RaceType)))
                {
                    _raceRepository.RaceType = (RaceType) value;
                    var athletePoints = _raceRepository.FindByAthleteId(athlete.Id);
                    if (athletePoints != null) points += athletePoints.Points;
                }

                var athleteResult = new Result(athlete.Id, athlete.FullName, athlete.FullNameReversed, points);
                athletesWithResults.Add(athleteResult);
            }

            return athletesWithResults.OrderBy(r => r.ReversedName).ToList();
        }

        public void AddResult(Referee referee, int athleteId, int points)
        {
            _raceRepository.RaceType = referee.RaceType;
            var athletePoints = _raceRepository.FindByAthleteId(athleteId);
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

            foreach (var observer in _observers)
            {
                Task.Run(() => _observers[observer.Key].ResultAdded(GetAthletesWithTotalPoints()));
            }
        }

        public List<Result> GetParticipantsWithResultInRace(RaceType raceType)
        {
            _raceRepository.RaceType = raceType;
            var results = (from athletePoints in _raceRepository.FindAllWithPoints() let athlete = _athleteRepository.FindOne(athletePoints.AthleteId) select new Result(athlete.Id, athlete.FullName, athlete.FullNameReversed, athletePoints.Points)).ToList();
            return results.OrderByDescending(r => r.Points).ToList();
        }
    }
}