using System;

namespace Model.Person
{
    // Person

    [Serializable]
    public class Person : Entity<int>
    {
    
        // Properties

        private string _firstName;
        private string _lastName;

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

        public string FullName
        {
            get => _firstName + " " + _lastName;
        }

        public string FullNameReversed
        {
            get => _lastName + " " + _firstName;
        }

        // Lifecycle

        protected Person(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }
    
        // Override Methods

        public override string ToString()
        {
            return Id + ", " + _firstName + " " + _lastName;
        }
    
    }
}