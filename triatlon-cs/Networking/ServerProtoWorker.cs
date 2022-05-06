using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Google.Protobuf;
using Service;
using Triatlon.Proto;

namespace Networking
{
    public class TriatlonClientProtoWorker : ITriatlonObserver
    {
        private readonly ITriatlonService _server;
        private readonly TcpClient _connection;
        private readonly NetworkStream _stream;
        private volatile bool _connected;

        public TriatlonClientProtoWorker(ITriatlonService server, TcpClient connecction)
        {
            _server = server;
            _connection = connecction;
            try
            {
                _stream = _connection.GetStream();
                _connected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public virtual void run()
        {
            while (_connected)
            {
                try
                {
                    var request = Request.Parser.ParseDelimitedFrom(_stream);
                    var response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                Thread.Sleep(1000);
            }
            _stream.Close();
            _connection.Close();
        }

        public void ResultAdded(List<Result> results)
        {
            SendResponse(new Response
            {
                Type = Response.Types.Type.ResultAdded,
                Results = { results.ToList() }
            });
        }

        private Response HandleRequest(Request request)
        {
            Response response = null;
            var requestType = request.Type;
            switch (requestType)
            {
                case Request.Types.Type.Login:
                {
                    try
                    {
                        Referee referee;
                        lock (_server)
                        {
                            referee = _server.LogInReferee(request.Referee.Email, request.Referee.Password, this);
                        }

                        return new Response
                        {
                            Type = Response.Types.Type.Login,
                            Referee = referee
                        };
                    }
                    catch (Exception ex)
                    {
                        _connected = false;
                        return new Response
                        {
                            Type = Response.Types.Type.Error,
                        };
                    }
                }
                case Request.Types.Type.Logout:
                {
                    try
                    {
                        lock (_server)
                        {
                            _server.LogOutReferee(request.Email);
                        }

                        _connected = false;
                        return new Response
                        {
                            Type = Response.Types.Type.Ok,
                        };
                    }
                    catch (Exception e)
                    {
                        return new Response
                        {
                            Type = Response.Types.Type.Error,
                        };
                    }
                }
                case Request.Types.Type.Athletes:
                {
                    try
                    {
                        List<Athlete> athletes;
                        lock (_server)
                        {
                            athletes = _server.GetAthletes();
                        }
                        
                        return new Response
                        {
                            Type = Response.Types.Type.Athletes,
                            Athletes = { athletes.ToList() }
                        };
                    }
                    catch (Exception e)
                    {
                        return new Response
                        {
                            Type = Response.Types.Type.Error,
                        };
                    }
                }
                case Request.Types.Type.AthletesWithTotalPoints:
                {
                    try
                    {
                        List<Result> results;
                        lock (_server)
                        {
                            results = _server.GetAthletesWithTotalPoints();
                        }
                        
                        return new Response
                        {
                            Type = Response.Types.Type.AthletesWithPoints,
                            Results = { results.ToList() }
                        };
                    }
                    catch (Exception e)
                    {
                        return new Response
                        {
                            Type = Response.Types.Type.Error,
                        };
                    }
                }
                case Request.Types.Type.AddResult:
                {
                    try
                    {
                        lock (_server)
                        {
                            _server.AddResult(request.ResultDTO.Referee, request.ResultDTO.AthleteId, request.ResultDTO.Points);
                        }
                        
                        return new Response
                        {
                            Type = Response.Types.Type.Ok,
                        };
                    }
                    catch (Exception e)
                    {
                        return new Response
                        {
                            Type = Response.Types.Type.Error,
                        };
                    }
                }
                case Request.Types.Type.ParticipantsWithResultInRace:
                {
                    try
                    {
                        List<Result> results;
                        lock (_server)
                        {
                            results = _server.GetParticipantsWithResultInRace(request.RaceType);
                        }
                        
                        return new Response
                        {
                            Type = Response.Types.Type.ParticipantsWithResultInRace,
                            Results = { results.ToList() }
                        };
                    }
                    catch (Exception e)
                    {
                        return new Response
                        {
                            Type = Response.Types.Type.Error,
                        };
                    }
                }
                default: return new Response {Type = Response.Types.Type.Error};
            }
        }

        private void SendResponse(Response response)
        {
            lock (_stream)
            {
                response.WriteDelimitedTo(_stream);
                _stream.Flush();
            }
        }
    }
}