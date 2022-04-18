using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Repository.Utils
{
    public abstract class ConnectionFactory
    {
    
        // Properties

        private static ConnectionFactory _instance;

        public static ConnectionFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Type[] types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if (type.IsSubclassOf(typeof(ConnectionFactory)))
                            _instance = (ConnectionFactory) Activator.CreateInstance(type);
                    }
                }
                return _instance;
            }
        }
    
        // Lifecycle
    
        protected ConnectionFactory() {}
    
        // Abstract Methods

        public abstract IDbConnection CreateConnection(IDictionary<string, string> properties);
    
    }
}