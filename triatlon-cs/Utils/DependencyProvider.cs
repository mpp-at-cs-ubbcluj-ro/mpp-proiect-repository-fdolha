using System.Collections.Generic;
using System.Configuration;
using System.IO;
using log4net.Config;

public class DependencyProvider
{
    
    // Private Properties

    private RaceRepository _raceRepository;
    private IAthleteRepository _athleteRepository;
    private IRefereeRepository _refereeRepository;
    private TriatlonService _triatlonService;
    
    // Static Properties

    private static DependencyProvider _shared = null;

    public static DependencyProvider Shared
    {
        get {
            if (_shared == null)
            {
                _shared = new DependencyProvider();
            }
            return _shared;
        }
    }
    
    // Lifecycle

    private DependencyProvider()
    {
        XmlConfigurator.Configure(new FileInfo("app.config"));
        IDictionary<string, string> properties = new SortedList<string, string>();
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["triatlonDB"];
        properties.Add("ConnectionString", settings.ConnectionString);
        _raceRepository = new RaceDbRepository(properties, RaceType.Swimming);
        _athleteRepository = new AthleteDbRepository(properties);
        _refereeRepository = new RefereeDbRepository(properties);
        _triatlonService = new TriatlonService(_raceRepository, _refereeRepository, _athleteRepository);
    }
    
    // Instance Methods

    public TriatlonService GetSharedService()
    {
        return _triatlonService;
    }

    public TriatlonService GetNewService()
    {
        return new TriatlonService(_raceRepository, _refereeRepository, _athleteRepository);
    }

}