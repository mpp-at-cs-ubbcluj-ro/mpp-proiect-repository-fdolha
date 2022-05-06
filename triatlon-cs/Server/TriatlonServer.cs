
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using log4net.Config;
using Networking;
using Networking.Utils;
using Service;

namespace Server
{
    class TriatlonServer
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("app.config"));
            IDictionary<string, string> properties = new SortedList<string, string>();
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["triatlon"];
            properties.Add("ConnectionString", settings.ConnectionString);
            var service = new TriatlonService(properties);
            var server = new SerialTriatlonServer("127.0.0.1", 55556, service);
            server.Start();
        }
    }

    public class SerialTriatlonServer : ConcurrentServer
    {
        private readonly ITriatlonService _server;
        private TriatlonClientProtoWorker _worker;

        public SerialTriatlonServer(string host, int port, ITriatlonService server) : base(host, port)
        {
            _server = server;
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            _worker = new TriatlonClientProtoWorker(_server, client);
            return new Thread(_worker.run);
        }
    }
}