using System.Data;

namespace triatlon_cs.Utils.Database;

// DbUtils

public class DbUtils
{
    
    // Properties

    private static IDbConnection? _connection;
    
    // Public Class Methods

    public static IDbConnection GetConnection(IDictionary<string, string> properties)
    {
        if (_connection == null || _connection.State == ConnectionState.Closed)
        {
                _connection = CreateNewConnection(properties);
                _connection.Open();
        }
        return _connection;
    }
    
    // Private Class Methods

    private static IDbConnection CreateNewConnection(IDictionary<string, string> properties)
    {
        return ConnectionFactory.Instance.CreateConnection(properties);
    }

}