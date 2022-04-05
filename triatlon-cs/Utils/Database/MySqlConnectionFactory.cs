using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

// MySqlConnectionFactory

public class MySqlConnectionFactory : ConnectionFactory
{
    
    // Override Methods
    
    public override IDbConnection CreateConnection(IDictionary<string, string> properties)
    {
        var connectionString = properties["ConnectionString"];
        Console.WriteLine("MySql - opening connect at ... {0}", connectionString);
        return new MySqlConnection(connectionString);
    }
    
}