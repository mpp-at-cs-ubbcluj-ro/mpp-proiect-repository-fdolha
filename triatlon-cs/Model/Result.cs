using System;

namespace Model
{
    [Serializable]
    public class Result
    {
    
        // Properties

        private int _id;
        private string _name;
        private int _points;
        private string _reversedName;

        public int Id
        {
            get => _id;
            set => _id = value;
        }
    
        public string Name
        {
            get => _name;
            set => _name = value;
        }
    
        public int Points
        {
            get => _points;
            set => _points = value;
        }
    
        public string ReversedName
        {
            get => _reversedName;
            set => _reversedName = value;
        }
    
        // Lifecycle

        public Result(int id, string name, string reversedName, int points)
        {
            _id = id;
            _points = points;
            _reversedName = reversedName;
            _name = name;
        }

    }
}