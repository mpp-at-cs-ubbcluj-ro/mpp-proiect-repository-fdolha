using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using Repository.DB;
using Service;
using Triatlon.Proto;
using Athlete = Model.Person.Athlete;
using AthletePoints = Model.Activity.AthletePoints;
using RaceType = Model.Activity.RaceType;
using Referee = Model.Person.Referee;
using Result = Model.Result;

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
        
        public Triatlon.Proto.Referee LogInReferee(string email, string password, ITriatlonObserver observer)
        {
            var referee = _refereeRepository.FindByEmail(email);
            if (referee == null) return null;
            var isLoggedIn = referee.Email.Equals(email) && referee.Password.Equals(password);
            if (isLoggedIn)
            {
                _observers[referee.Email] = observer;
            }
            var refereeProto = new Triatlon.Proto.Referee
            {
                Id = referee.Id,
                FirstName = referee.FirstName,
                LastName = referee.LastName,
                RaceType = ProtoUtils.ConvertRaceType(referee.RaceType),
                Email = referee.Email,
                Password = referee.Password
            };
            return refereeProto;
        }

        public void LogOutReferee(string email)
        {
            _observers.Remove(email);
        }

        public List<Triatlon.Proto.Athlete> GetAthletes()
        {
            var athletes = new List<Triatlon.Proto.Athlete>();
            foreach (var athlete in _athleteRepository.FindAll())
            {
                athletes.Add(ProtoUtils.ConvertAthlete(athlete));
            }

            return athletes;
        }

        public List<Triatlon.Proto.Result> GetAthletesWithTotalPoints()
        {
            var athletesWithResults = new List<Triatlon.Proto.Result>();
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
                athletesWithResults.Add(ProtoUtils.ConvertResult(athleteResult));
            }

            return athletesWithResults.OrderBy(r => r.ReversedName).ToList();
        }

        public void AddResult(Triatlon.Proto.Referee referee, int athleteId, int points)
        {
            _raceRepository.RaceType = ProtoUtils.ConvertBackRaceType(referee.RaceType);
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

        public List<Triatlon.Proto.Result> GetParticipantsWithResultInRace(Triatlon.Proto.RaceType raceType)
        {
            _raceRepository.RaceType = ProtoUtils.ConvertBackRaceType(raceType);
            var results = (from athletePoints in _raceRepository.FindAllWithPoints() let athlete = _athleteRepository.FindOne(athletePoints.AthleteId) select new Result(athlete.Id, athlete.FullName, athlete.FullNameReversed, athletePoints.Points)).ToList();
            var resultsProto = new List<Triatlon.Proto.Result>();
            foreach (var result in results.OrderByDescending(r => r.Points))
            {
                resultsProto.Add(ProtoUtils.ConvertResult(result));
            }

            return resultsProto;
        }
    }
}