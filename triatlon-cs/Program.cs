using System.Configuration;
using log4net.Config;
using triatlon_cs.Repository.DB;

public class TriatlonProgram
{
    public static void Main(string[] args)
    {
        XmlConfigurator.Configure(new FileInfo(args[0]));
        IDictionary<string, string> properties = new SortedList<string, string>();
        properties.Add("ConnectionString", GetConnectionStringByName("triatlonDB"));
        AthleteDbRepository athleteDbRepository = new AthleteDbRepository(properties);
        foreach (var athlete in athleteDbRepository.FindAll())
        {
            Console.WriteLine(athlete);
        }
    }

    static string GetConnectionStringByName(string name)
    {
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
        return settings.ConnectionString;
    }
}