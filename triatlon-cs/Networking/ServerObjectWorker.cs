using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Model;
using Model.Person;
using Networking.DTO;
using Service;

namespace Networking
{
    public class TriatlonClientWorker : ITriatlonObserver
    {
        private readonly ITriatlonService _server;
        private readonly TcpClient _connection;
        private readonly NetworkStream _stream;
        private readonly IFormatter _formatter;
        private volatile bool _connected;

        public TriatlonClientWorker(ITriatlonService server, TcpClient connection)
        {
            _server = server;
            _connection = connection;
            try
            {
                _stream = _connection.GetStream();
                _formatter = new BinaryFormatter();
                _connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void Run()
        {
            while (_connected)
            {
                try
                {
                    var request = _formatter.Deserialize(_stream);
                    var response = HandleRequest((IRequest) request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                Thread.Sleep(1000);
            }
            _stream.Close();
            _connection.Close();
        }

        public void ResultAdded(List<Result> results)
        {
            SendResponse(new ResultAddedResponse(results));
        }

        private IResponse HandleRequest(IRequest request)
        {
            switch (request)
            {
                case LogInRequest logInRequest:
                {
                    var refereeDto = logInRequest.RefereeDto;
                    try
                    {
                        lock (_server)
                        {
                            _server.LogInReferee(refereeDto.Email, refereeDto.Password, this);
                        }

                        return new OkResponse();
                    }
                    catch (Exception e)
                    {
                        _connected = false;
                        return new ErrorResponse();
                    }
                }
                case LogOutRequest logOutRequest:
                {
                    var email = logOutRequest.Email;
                    try
                    {
                        lock (_server)
                        {
                            _server.LogOutReferee(email);
                        }

                        _connected = false;
                        return new OkResponse();
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse();
                    }
                }
                case AthletesRequest:
                    try
                    {
                        List<Athlete> athletes;
                        lock (_server)
                        {
                            athletes = _server.GetAthletes();
                        }

                        return new AthletesResponse(athletes);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse();
                    }
                case AthletesWithTotalPointsRequest:
                    try
                    {
                        List<Result> results;
                        lock (_server)
                        {
                            results = _server.GetAthletesWithTotalPoints();
                        }

                        return new AthletesWithTotalPointsResponse(results);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse();
                    }
                case AddResultRequest addResultRequest:
                {
                    var resultDto = addResultRequest.ResultDto;
                    try
                    {
                        lock (_server)
                        {
                            _server.AddResult(resultDto.Referee, resultDto.AthleteId, resultDto.Points);
                        }

                        return new OkResponse();
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse();
                    }
                }
                case AthletesWithResultInRaceRequest athletesWithResultInRaceRequest:
                    try
                    {
                        List<Result> results;
                        lock (_server)
                        {
                            results = _server.GetParticipantsWithResultInRace(athletesWithResultInRaceRequest.RaceType);
                        }

                        return new AthletesWithTotalPointsResponse(results);
                    }
                    catch (Exception e)
                    {
                        return new ErrorResponse();
                    }
                default:
                    return new ErrorResponse();
            }
        }

        private void SendResponse(IResponse response)
        {
            lock (_stream)
            {
                _formatter.Serialize(_stream, response);
                _stream.Flush();
            }
        }
    }
}