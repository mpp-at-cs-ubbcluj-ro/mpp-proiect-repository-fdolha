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
using Networking.DTO;

namespace Networking
{
    /*
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
            InitializeConnection();
            var refereeDto = new RefereeDto(email, password);
            var request = new LogInRequest(refereeDto);
            SendRequest(request);
            var response = ReadResponse();
            Referee referee = null;
            if (response is LogInResponse)
            {
                var refDto = ((LogInResponse)response).RefereeDto;
                referee = new Referee(refDto.FirstName, refDto.LastName, refDto.RaceType, refDto.Email, refDto.Password);
                _client = observer;
            }
            else CloseConnection();
            return referee;
        }

        public void LogOutReferee(string email)
        {
            InitializeConnection();
            var request = new LogOutRequest(email);
            SendRequest(request);
            CloseConnection();
        }

        public List<Athlete> GetAthletes()
        {
            var request = new AthletesRequest();
            SendRequest(request);
            var response = (AthletesResponse) ReadResponse();
            return response.Athletes;
        }

        public List<Result> GetAthletesWithTotalPoints()
        {
            if (_connection == null) InitializeConnection();
            var request = new AthletesWithTotalPointsRequest();
            SendRequest(request);
            var response = (AthletesWithTotalPointsResponse) ReadResponse();
            return response.Results;
        }

        public void AddResult(Referee referee, int athleteId, int points)
        {
            var resultDto = new ResultDto(referee, athleteId, points);
            var request = new AddResultRequest(resultDto);
            SendRequest(request);
            ReadResponse();
        }

        public List<Result> GetParticipantsWithResultInRace(RaceType raceType)
        {
            var request = new AthletesWithResultInRaceRequest(raceType);
            SendRequest(request);
            var response = (AthletesWithResultInRaceResponse) ReadResponse();
            return response.Results;
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
    */
}