using System;
using Model.Person;

namespace Networking.DTO
{
    [Serializable]
    public class ResultDto
    {
        private Referee _referee;
        private int _athleteId;
        private int _points;

        public ResultDto(Referee referee, int athleteId, int points)
        {
            _referee = referee;
            _athleteId = athleteId;
            _points = points;
        }

        public ResultDto(int athleteId, int points)
        {
            _athleteId = athleteId;
            _points = points;
        }
        
        public Referee Referee
        {
            get => _referee;
            set => _referee = value;
        }
        
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
    }
}