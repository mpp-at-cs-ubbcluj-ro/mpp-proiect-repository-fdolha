using System;

namespace Triatlon.Proto
{
    public class ProtoUtils
    {
        public static RaceType ConvertRaceType(Model.Activity.RaceType raceType)
        {
            switch (raceType)
            {
                case Model.Activity.RaceType.Swimming: return RaceType.Swimming;
                case Model.Activity.RaceType.Cycling: return RaceType.Cycling;
                case Model.Activity.RaceType.Running: return RaceType.Running;
                default:
                    throw new ArgumentOutOfRangeException(nameof(raceType), raceType, null);
            }
        }

        public static Model.Activity.RaceType ConvertBackRaceType(RaceType raceType)
        {
            switch (raceType)
            {
                case RaceType.Swimming: return Model.Activity.RaceType.Swimming;
                case RaceType.Cycling: return Model.Activity.RaceType.Cycling;
                case RaceType.Running: return Model.Activity.RaceType.Running;
                default:
                    throw new ArgumentOutOfRangeException(nameof(raceType), raceType, null);
            }
        }

        public static Athlete ConvertAthlete(Model.Person.Athlete athlete)
        {
            return new Athlete
            {
                Id = athlete.Id,
                FirstName = athlete.FirstName,
                LastName = athlete.LastName
            };
        }

        public static Result ConvertResult(Model.Result result)
        {
            return new Result
            {
                Id = result.Id,
                Name = result.Name,
                Points = result.Points,
                ReversedName = result.ReversedName
            };
        }
    }
}