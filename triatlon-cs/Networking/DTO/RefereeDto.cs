using System;
using Model.Activity;

namespace Networking.DTO
{
    [Serializable]
    public class RefereeDto
    {
        private string _firstName;
        private string _lastName;
        private RaceType _raceType;
        private string _email;
        private string _password;

        public RefereeDto(string email, string password)
        {
            _email = email;
            _password = password;
        }

        public RefereeDto(string firstName, string lastName, RaceType raceType, string email, string password)
        {
            _firstName = firstName;
            _lastName = lastName;
            _raceType = raceType;
            _email = email;
            _password = password;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }
        
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }
        
        public RaceType RaceType
        {
            get => _raceType;
            set => _raceType = value;
        }
        
        public string Email
        {
            get => _email;
            set => _email = value;
        }
        
        public string Password
        {
            get => _password;
            set => _password = value;
        }
    }
}