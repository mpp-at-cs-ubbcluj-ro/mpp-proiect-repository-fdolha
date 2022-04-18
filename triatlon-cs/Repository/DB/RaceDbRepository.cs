using System.Collections.Generic;
using System.Data;
using log4net;
using Model.Activity;
using Repository.Utils;

namespace Repository.DB
{
    public class RaceDbRepository : RaceRepository
{

    // Private Properties

    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    private readonly IDictionary<string, string> _properties;
    
    // Lifecycle

    public RaceDbRepository(IDictionary<string, string> properties, RaceType raceType) : base(raceType)
    {
        Logger.Info("Creating RaceDbRepository");
        _properties = properties;
    }
    
    public RaceDbRepository(IDictionary<string, string> properties, Race race) : base(race)
    {
        Logger.Info("Creating RaceDbRepository");
        _properties = properties;
    }
    
    // Private Methods

    private string tableNameForRaceType(RaceType raceType)
    {
        switch (raceType)
        {
            case RaceType.Swimming: return "swimming_race";
            case RaceType.Cycling: return "cycling_race";
            case RaceType.Running: return "running_race";
            default: return "";
        }
    }
    
    // Override Methods (RaceRepository)
    
    public override AthletePoints FindByAthleteId(int athleteId)
    {
        Logger.InfoFormat("Entering FindByAthleteId with value {0}", athleteId);
        IDbConnection connection = DbUtils.GetConnection(_properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"select * from {tableNameForRaceType(RaceType)} where athleteId=@athleteId";
            var parameterId = command.CreateParameter();
            parameterId.ParameterName = "@athleteId";
            parameterId.Value = athleteId;
            command.Parameters.Add(parameterId);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    int points = dataReader.GetInt32(2);
                    var athletePoints = new AthletePoints(athleteId, points);
                    athletePoints.Id = _id;
                    Logger.InfoFormat("Exiting FindByAthleteId with value {0}", athletePoints);
                    return athletePoints;
                }
            }
        }
        Logger.InfoFormat("Exiting FindByAthleteId with value {0}", null);
        return null;
    }

    public override IEnumerable<AthletePoints> FindAllWithPoints()
    {
        Logger.Info("Entering FindAllWithPoints");
        IDbConnection connection = DbUtils.GetConnection(_properties);
        IList<AthletePoints> athletesPoints = new List<AthletePoints>();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"select * from {tableNameForRaceType(RaceType)} where points > 0";
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    int athleteId = dataReader.GetInt32(1);
                    int points = dataReader.GetInt32(2);
                    var athletePoints = new AthletePoints(athleteId, points);
                    athletePoints.Id = _id;
                    athletesPoints.Add(athletePoints);
                }
            }
        }
        Logger.Info("Exiting FindAllWithPoints");
        return athletesPoints;
    }
    
    // Override Methods (Repository)

    public override AthletePoints FindOne(int id)
    {
        Logger.InfoFormat("Entering FindOne with value {0}", id);
        IDbConnection connection = DbUtils.GetConnection(_properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"select * from {tableNameForRaceType(RaceType)} where id=@id";
            var parameterId = command.CreateParameter();
            parameterId.ParameterName = "@id";
            parameterId.Value = id;
            command.Parameters.Add(parameterId);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    int athleteId = dataReader.GetInt32(1);
                    int points = dataReader.GetInt32(2);
                    var athletePoints = new AthletePoints(athleteId, points);
                    athletePoints.Id = _id;
                    Logger.InfoFormat("Exiting FindOne with value {0}", athletePoints);
                    return athletePoints;
                }
            }
        }
        Logger.InfoFormat("Exiting FindOne with value {0}", null);
        return null;
    }

    public override IEnumerable<AthletePoints> FindAll()
    {
        Logger.Info("Entering FindAll");
        IDbConnection connection = DbUtils.GetConnection(_properties);
        IList<AthletePoints> athletesPoints = new List<AthletePoints>();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"select * from {tableNameForRaceType(RaceType)}";
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    int athleteId = dataReader.GetInt32(1);
                    int points = dataReader.GetInt32(2);
                    var athletePoints = new AthletePoints(athleteId, points);
                    athletePoints.Id = _id;
                    athletesPoints.Add(athletePoints);
                }
            }
        }
        Logger.Info("Exiting FindAll");
        return athletesPoints;
    }

    public override AthletePoints Save(AthletePoints entity)
    {
        Logger.InfoFormat("Entering Save with value {0}", entity);
        IDbConnection connection = DbUtils.GetConnection(_properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"insert into {tableNameForRaceType(RaceType)} (athleteId, points) values (@athleteId, @points)";
            var athleteIdParameter = command.CreateParameter();
            athleteIdParameter.ParameterName = "@athleteId";
            athleteIdParameter.Value = entity.AthleteId;
            command.Parameters.Add(athleteIdParameter);
            var pointsParameter = command.CreateParameter();
            pointsParameter.ParameterName = "@points";
            pointsParameter.Value = entity.Points;
            command.Parameters.Add(pointsParameter);
            var result = command.ExecuteNonQuery();
            Logger.InfoFormat("Exiting Save with value {0}", result);
        }
        return entity;
    }

    public override int Delete(int id)
    {
        Logger.InfoFormat("Entering Delete with value {0}", id);
        IDbConnection connection = DbUtils.GetConnection(_properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"delete from {tableNameForRaceType(RaceType)} where id=@id";
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);
            var result = command.ExecuteNonQuery();
            Logger.InfoFormat("Exiting Delete with value {0}", result);
        }
        return id;
    }

    public override AthletePoints Update(AthletePoints entity)
    {
        Logger.InfoFormat("Entering Update with value {0}", entity);
        IDbConnection connection = DbUtils.GetConnection(_properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"update {tableNameForRaceType(RaceType)} set athleteId=@athleteId, points=@points where id=@id";
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = entity.Id;
            command.Parameters.Add(idParameter);
            var athleteIdParameter = command.CreateParameter();
            athleteIdParameter.ParameterName = "@athleteId";
            athleteIdParameter.Value = entity.AthleteId;
            command.Parameters.Add(athleteIdParameter);
            var pointsParameter = command.CreateParameter();
            pointsParameter.ParameterName = "@points";
            pointsParameter.Value = entity.Points;
            command.Parameters.Add(pointsParameter);
            var result = command.ExecuteNonQuery();
            Logger.InfoFormat("Exiting Update with value {0}", result);
        }
        return entity;
    }
    
}
}