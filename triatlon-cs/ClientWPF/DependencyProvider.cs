using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Networking;
using Service;

namespace ClientWPF
{
    internal sealed class DependencyProvider
    {
        private static DependencyProvider? _instance;
        private ITriatlonService _server;
        public static DependencyProvider Instance
        {
            get => _instance ?? (_instance = new DependencyProvider());
        }

        private DependencyProvider()
        { 
            _server = new TriatlonServerProxy("127.0.0.1", 55556);
        }

        public ITriatlonService GetServer()
        {
            return _server;
        }
    }
}
