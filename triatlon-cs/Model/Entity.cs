using System;

namespace Model
{
    // Entity

    [Serializable]
    public class Entity<TId>
    {
    
        // Properties

        private TId _id;

        public TId Id
        {
            get => _id;
            set => _id = value;
        }
        
    }
}