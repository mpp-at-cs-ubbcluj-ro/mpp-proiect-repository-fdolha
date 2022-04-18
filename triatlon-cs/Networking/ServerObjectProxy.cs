using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Model;
using Model.Activity;
using Model.Person;
using Service;

namespace Networking
{
    public class TriatlonServerProxy : ITriatlonService
    {
        private readonly string _host;
        private readonly int _port;
        private ITriatlonObserver _client;
        private NetworkStream _stream;
        private IFormatter _formatter;
        private TcpClient _connection;
        private readonly Queue<IResponse> _responses;
        private volatile bool _finished;
        private EventWaitHandle _waitHandle;

        public TriatlonServerProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new Queue<IResponse>();
        }
        
        protected virtual void Run()
        {
            while (!_finished)
            {
                try
                {
                    var response = _formatter.Deserialize(_stream);
                    if (response is IUpdateResponse)
                    {
                        HandleUpdate((IUpdateResponse) response);
                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue((IResponse) response);
                        }

                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        public Referee LogInReferee(string email, string password, ITriatlonObserver observer)
        {
            throw new System.NotImplementedException();
        }

        public void LogOutReferee(string email)
        {
            throw new System.NotImplementedException();
        }

        public List<Athlete> GetAthletes()
        {
            throw new System.NotImplementedException();
        }

        public List<Result> GetAthletesWithTotalPoints()
        {
            throw new System.NotImplementedException();
        }

        public void AddResult(Referee referee, int athleteId, int points)
        {
            throw new System.NotImplementedException();
        }

        public List<Result> GetParticipantsWithResultInRace(RaceType raceType)
        {
            throw new System.NotImplementedException();
        }

        private void InitializeConnection()
        {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _finished = false;
                _waitHandle = new AutoResetEvent(false);
                StartReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream.Close();
                _connection.Close();
                _waitHandle.Close();
                _client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void SendRequest(IRequest request)
        {
            try
            {
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private IResponse ReadResponse()
        {
            IResponse response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (_responses)
                {
                    response = _responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }

        private void StartReader()
        {
            var thread = new Thread(Run);
            thread.Start();
        }

        private void HandleUpdate(IUpdateResponse updateResponse)
        {
            if (updateResponse is ResultAddedResponse)
            {
                var resultAddedResponse = (ResultAddedResponse) updateResponse;
                var results = resultAddedResponse.Results;
                try
                {
                    _client.ResultAdded(results);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}