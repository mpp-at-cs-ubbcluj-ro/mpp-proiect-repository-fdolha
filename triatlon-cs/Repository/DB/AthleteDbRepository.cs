using System.Data;
using log4net;
using triatlon_cs.Model.Person;
using triatlon_cs.Utils.Database;

namespace triatlon_cs.Repository.DB;

// AthleteDbRepository

public class AthleteDbRepository : IAthleteRepository
{
    
    // Private Properties

    private static readonly ILog logger = LogManager.GetLogger("AthleteDbRepository");
    private IDictionary<string, string> properties;
    
    // Lifecycle

    public AthleteDbRepository(IDictionary<string, string> properties)
    {
        logger.Info("Creating AthleteDbRepository");
        this.properties = properties;
    }

    // Repository Methods
    
    public Athlete FindOne(int id)
    {
        logger.InfoFormat("Entering FindOne with value {0}", id);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select * from athletes where id=@id";
            var parameterId = command.CreateParameter();
            parameterId.ParameterName = "@id";
            parameterId.Value = id;
            command.Parameters.Add(parameterId);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    string firstName = dataReader.GetString(1);
                    string lastName = dataReader.GetString(2);
                    var athlete = new Athlete(firstName, lastName);
                    athlete.Id = _id;
                    logger.InfoFormat("Exiting FindOne with value {0}", athlete);
                    return athlete;
                }
            }
        }
        logger.InfoFormat("Exiting FindOne with value {0}", null);
        return null;
    }

    public IEnumerable<Athlete> FindAll()
    {
        logger.Info("Entering FindAll");
        IDbConnection connection = DbUtils.GetConnection(properties);
        IList<Athlete> athletes = new List<Athlete>();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select * from athletes";
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    string firstName = dataReader.GetString(1);
                    string lastName = dataReader.GetString(2);
                    var athlete = new Athlete(firstName, lastName);
                    athlete.Id = _id;
                    athletes.Add(athlete);
                }
            }
        }
        logger.Info("Exiting FindAll");
        return athletes;
    }

    public Athlete Save(Athlete entity)
    {
        logger.InfoFormat("Entering Save with value {0}", entity);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "insert into athletes (firstName, lastName) values (@firstName, @lastName)";
            var firstNameParameter = command.CreateParameter();
            firstNameParameter.ParameterName = "@firstName";
            firstNameParameter.Value = entity.FirstName;
            command.Parameters.Add(firstNameParameter);
            var lastNameParameter = command.CreateParameter();
            lastNameParameter.ParameterName = "@lastName";
            lastNameParameter.Value = entity.LastName;
            command.Parameters.Add(lastNameParameter);
            var result = command.ExecuteNonQuery();
            logger.InfoFormat("Exiting Save with value {0}", result);
        }
        return entity;
    }

    public int Delete(int id)
    {
        logger.InfoFormat("Entering Delete with value {0}", id);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "delete from athletes where id=@id";
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);
            var result = command.ExecuteNonQuery();
            logger.InfoFormat("Exiting Delete with value {0}", result);
        }
        return id;
    }

    public Athlete Update(Athlete entity)
    {
        logger.InfoFormat("Entering Update with value {0}", entity);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "update athletes set firstName=@firstName, lastName=@lastName where id=@id";
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = entity.Id;
            command.Parameters.Add(idParameter);
            var firstNameParameter = command.CreateParameter();
            firstNameParameter.ParameterName = "@firstName";
            firstNameParameter.Value = entity.FirstName;
            command.Parameters.Add(firstNameParameter);
            var lastNameParameter = command.CreateParameter();
            lastNameParameter.ParameterName = "@lastName";
            lastNameParameter.Value = entity.LastName;
            command.Parameters.Add(lastNameParameter);
            var result = command.ExecuteNonQuery();
            logger.InfoFormat("Exiting Update with value {0}", result);
        }
        return entity;
    }
}