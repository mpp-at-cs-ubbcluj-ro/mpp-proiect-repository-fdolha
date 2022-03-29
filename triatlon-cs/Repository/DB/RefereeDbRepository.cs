using System.Data;
using log4net;
using triatlon_cs.Model.Activity;
using triatlon_cs.Model.Person;
using triatlon_cs.Utils.Database;

namespace triatlon_cs.Repository.DB;

// RefereeDbRepository

public class RefereeDbRepository : IRefereeRepository
{
    
    // Private Properties

    private static readonly ILog logger = LogManager.GetLogger("RefereeDbRepository");
    private IDictionary<string, string> properties;
    
    // Lifecycle

    public RefereeDbRepository(IDictionary<string, string> properties)
    {
        logger.Info("Creating RefereeDbRepository");
        this.properties = properties;
    }
    
    // Repository Methods (IRefereeRepository)

    public Referee FindByEmail(string email)
    {
        logger.InfoFormat("Entering FindEmail with value {0}", email);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select * from referees where email=@email";
            var parameterId = command.CreateParameter();
            parameterId.ParameterName = "@email";
            parameterId.Value = email;
            command.Parameters.Add(parameterId);
            using (var dataReader = command.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    string firstName = dataReader.GetString(1);
                    string lastName = dataReader.GetString(2);
                    RaceType raceType = (RaceType) dataReader.GetInt32(3);
                    string password = dataReader.GetString(5);
                    var referee = new Referee(firstName, lastName, raceType, email, password);
                    referee.Id = _id;
                    logger.InfoFormat("Exiting FindOne with value {0}", referee);
                    return referee;
                }
            }
        }
        logger.InfoFormat("Exiting FindOne with value {0}", null);
        return null;
    }
    
    // Repository Methods
    
    public Referee FindOne(int id)
    {
        logger.InfoFormat("Entering FindOne with value {0}", id);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select * from referees where id=@id";
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
                    RaceType raceType = (RaceType) dataReader.GetInt32(3);
                    string email = dataReader.GetString(4);
                    string password = dataReader.GetString(5);
                    var referee = new Referee(firstName, lastName, raceType, email, password);
                    referee.Id = _id;
                    logger.InfoFormat("Exiting FindOne with value {0}", referee);
                    return referee;
                }
            }
        }
        logger.InfoFormat("Exiting FindOne with value {0}", null);
        return null;
    }

    public IEnumerable<Referee> FindAll()
    {
        logger.Info("Entering FindAll");
        IDbConnection connection = DbUtils.GetConnection(properties);
        IList<Referee> referees = new List<Referee>();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "select * from referees";
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    int _id = dataReader.GetInt32(0);
                    string firstName = dataReader.GetString(1);
                    string lastName = dataReader.GetString(2);
                    RaceType raceType = (RaceType) dataReader.GetInt32(3);
                    string email = dataReader.GetString(4);
                    string password = dataReader.GetString(5);
                    var referee = new Referee(firstName, lastName, raceType, email, password);
                    referee.Id = _id;
                    referees.Add(referee);
                }
            }
        }
        logger.Info("Exiting FindAll");
        return referees;
    }

    public Referee Save(Referee entity)
    {
        logger.InfoFormat("Entering Save with value {0}", entity);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "insert into referees (firstName, lastName, raceType, email, password) values (@firstName, @lastName, @raceType, @email, @password)";
            var firstNameParameter = command.CreateParameter();
            firstNameParameter.ParameterName = "@firstName";
            firstNameParameter.Value = entity.FirstName;
            command.Parameters.Add(firstNameParameter);
            var lastNameParameter = command.CreateParameter();
            lastNameParameter.ParameterName = "@lastName";
            lastNameParameter.Value = entity.LastName;
            command.Parameters.Add(lastNameParameter);
            var raceTypeParameter = command.CreateParameter();
            raceTypeParameter.ParameterName = "@raceType";
            raceTypeParameter.Value = (int) entity.RaceType;
            command.Parameters.Add(raceTypeParameter);
            var emailParameter = command.CreateParameter();
            emailParameter.ParameterName = "@email";
            emailParameter.Value = entity.Email;
            command.Parameters.Add(emailParameter);
            var passwordParameter = command.CreateParameter();
            passwordParameter.ParameterName = "@password";
            passwordParameter.Value = entity.Password;
            command.Parameters.Add(passwordParameter);
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
            command.CommandText = "delete from referees where id=@id";
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);
            var result = command.ExecuteNonQuery();
            logger.InfoFormat("Exiting Delete with value {0}", result);
        }
        return id;
    }

    public Referee Update(Referee entity)
    {
        logger.InfoFormat("Entering Update with value {0}", entity);
        IDbConnection connection = DbUtils.GetConnection(properties);
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "update referees set firstName=@firstName, lastName=@lastName, raceType=@raceType, email=@email, password=@password where id=@id";
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
            var raceTypeParameter = command.CreateParameter();
            raceTypeParameter.ParameterName = "@raceType";
            raceTypeParameter.Value = (int) entity.RaceType;
            command.Parameters.Add(raceTypeParameter);
            var emailParameter = command.CreateParameter();
            emailParameter.ParameterName = "@email";
            emailParameter.Value = entity.Email;
            command.Parameters.Add(emailParameter);
            var passwordParameter = command.CreateParameter();
            passwordParameter.ParameterName = "@password";
            passwordParameter.Value = entity.Password;
            command.Parameters.Add(passwordParameter);
            var result = command.ExecuteNonQuery();
            logger.InfoFormat("Exiting Update with value {0}", result);
        }
        return entity;
    }
    
}