using System.Collections.Generic;
using Model.Activity;

namespace Repository
{
    public abstract class RaceRepository : IRepository<int, AthletePoints>
    {
    
        // Properties

        private RaceType _raceType;

        public RaceType RaceType
        {
            get => _raceType;
            set => _raceType = value;
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
    
        // Abstract Methods

        public abstract AthletePoints FindByAthleteId(int athleteId);
        public abstract IEnumerable<AthletePoints> FindAllWithPoints();

        // IRepository Methods
    
        public abstract AthletePoints FindOne(int id);
        public abstract IEnumerable<AthletePoints> FindAll();
        public abstract AthletePoints Save(AthletePoints entity);
        public abstract int Delete(int id);
        public abstract AthletePoints Update(AthletePoints entity);
    
    }
}