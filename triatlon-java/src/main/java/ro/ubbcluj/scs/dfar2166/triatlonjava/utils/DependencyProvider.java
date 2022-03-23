package ro.ubbcluj.scs.dfar2166.triatlonjava.utils;

import ro.ubbcluj.scs.dfar2166.triatlonjava.model.activity.RaceType;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.person.Referee;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.AthleteRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.RaceRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.RefereeRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.db.AthleteDBRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.db.RaceDBRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.db.RefereeDBRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.service.TriatlonService;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class DependencyProvider {

    // Private Properties

    private static DependencyProvider instance = null;

    private final RaceRepository raceRepository;
    private final AthleteRepository athleteRepository;
    private final RefereeRepository refereeRepository;
    private final TriatlonService triatlonService;

    // Lifecycle

    private DependencyProvider() {
        Properties properties = new Properties();
        try {
            properties.load(new FileReader("bd.config"));
        } catch (IOException ignored) {}
        raceRepository = new RaceDBRepository(properties, RaceType.SWIMMING);
        athleteRepository = new AthleteDBRepository(properties);
        refereeRepository = new RefereeDBRepository(properties);
        triatlonService = new TriatlonService(raceRepository, refereeRepository, athleteRepository);
    }

    // Instance Methods

    public TriatlonService getSharedService() {
        return triatlonService;
    }

    public TriatlonService getNewService() {
        return new TriatlonService(raceRepository, refereeRepository, athleteRepository);
    }

    public void createAccount() {
        Referee referee = new Referee("Dorian", "Popa", RaceType.SWIMMING, "dorianpopa@trt.com", "123456");
        refereeRepository.save(referee);
    }

    // Class Methods

    public static DependencyProvider getInstance() {
        if (instance == null) instance = new DependencyProvider();
        return instance;
    }

}
