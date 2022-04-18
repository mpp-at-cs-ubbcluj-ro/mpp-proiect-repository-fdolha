using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Networking.Utils
{
    public abstract class AbstractServer
    {
        private TcpListener _server;
        private readonly string _host;
        private readonly int _port;

        protected AbstractServer(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Start()
        {
            var address = IPAddress.Parse(_host);
            var endPoint = new IPEndPoint(address, _port);
            _server = new TcpListener(endPoint);
            _server.Start();
            while (true)
            {
                TcpClient client = _server.AcceptTcpClient();
                ProcessRequest(client);
            }
        }

        protected abstract void ProcessRequest(TcpClient client);
    }

    public abstract class ConcurrentServer : AbstractServer
    {
        protected ConcurrentServer(string host, int port) : base(host, port) {}

        protected override void ProcessRequest(TcpClient client)
        {
            var thread = CreateWorker(client);
            thread.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}