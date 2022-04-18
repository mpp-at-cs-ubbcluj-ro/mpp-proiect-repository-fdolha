using System;
using Model.Activity;
using Networking.DTO;

namespace Networking
{
    public interface IRequest {}

    [Serializable]
    public class LogInRequest : IRequest
    {
        public LogInRequest(RefereeDto refereeDto)
        {
            RefereeDto = refereeDto;
        }

        public virtual RefereeDto RefereeDto { get; }
    }

    [Serializable]
    public class LogOutRequest : IRequest
    {
        public LogOutRequest(string email)
        {
            Email = email;
        }
            
        public virtual string Email { get; }
    }

    [Serializable]
    public class AthletesRequest : IRequest {}

    [Serializable]
    public class AthletesWithTotalPointsRequest : IRequest {}

    [Serializable]
    public class AddResultRequest : IRequest
    {
        public AddResultRequest(ResultDto resultDto)
        {
            ResultDto = resultDto;
        }
        
        public virtual ResultDto ResultDto { get; }
    }

    [Serializable]
    public class AthletesWithResultInRaceRequest : IRequest
    {
        public AthletesWithResultInRaceRequest(RaceType raceType)
        {
            RaceType = raceType;
        }
        
        public virtual RaceType RaceType { get; }
    }
    
}