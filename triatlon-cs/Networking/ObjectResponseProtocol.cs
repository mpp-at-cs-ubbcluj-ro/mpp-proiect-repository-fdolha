using System;
using System.Collections.Generic;
using Model;
using Model.Person;
using Networking.DTO;

namespace Networking
{
    public interface IResponse {}

    public interface IUpdateResponse : IResponse {}

    [Serializable]
    public class OkResponse : IResponse {}

    [Serializable]
    public class ErrorResponse : IResponse {}

    [Serializable]
    public class LogInResponse : IResponse
    {
        public LogInResponse(RefereeDto refereeDto)
        {
            RefereeDto = refereeDto;
        }

        public virtual RefereeDto RefereeDto { get; }
    }

    [Serializable]
    public class AthletesResponse : IResponse
    {
        public AthletesResponse(List<Athlete> athletes)
        {
            Athletes = athletes;
        }
        
        public virtual List<Athlete> Athletes { get; }
    }
    
    [Serializable]
    public class AthletesWithTotalPointsResponse : IResponse
    {
        public AthletesWithTotalPointsResponse(List<Result> results)
        {
            Results = results;
        }
        
        public virtual List<Result> Results { get; }
    }
    
    [Serializable]
    public class AthletesWithResultInRaceResponse : IResponse
    {
        public AthletesWithResultInRaceResponse(List<Result> results)
        {
            Results = results;
        }
        
        public virtual List<Result> Results { get; }
    }
    
    [Serializable]
    public class ResultAddedResponse : IUpdateResponse
    {
        public ResultAddedResponse(List<Result> results)
        {
            Results = results;
        }
        
        public virtual List<Result> Results { get; }
    }
    
}