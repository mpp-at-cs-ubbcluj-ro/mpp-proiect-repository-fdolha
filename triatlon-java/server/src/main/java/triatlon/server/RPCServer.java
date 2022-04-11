package triatlon.server;

import triatlon.model.activity.RaceType;
import triatlon.networking.utils.AbstractServer;
import triatlon.networking.utils.TriatlonRPCConcurrentServer;
import triatlon.repository.AthleteRepository;
import triatlon.repository.RaceRepository;
import triatlon.repository.RefereeRepository;
import triatlon.repository.db.AthleteDBRepository;
import triatlon.repository.db.RaceDBRepository;
import triatlon.repository.db.RefereeDBRepository;
import triatlon.service.TriatlonServiceInterface;

import java.util.Properties;

public class RPCServer {

    private static final int defaultPort = 55555;

    public static void main(String[] args) {
        Properties serverProperties = new Properties();
        try {
            serverProperties.load(RPCServer.class.getResourceAsStream("/triatlonserver.properties"));
        } catch (Exception ex) {
            ex.printStackTrace();
            return;
        }
        RaceRepository raceRepository = new RaceDBRepository(serverProperties, RaceType.SWIMMING);
        AthleteRepository athleteRepository = new AthleteDBRepository(serverProperties);
        RefereeRepository refereeRepository = new RefereeDBRepository(serverProperties);
        TriatlonServiceInterface service = new TriatlonService(raceRepository, refereeRepository, athleteRepository);
        int serverPort = defaultPort;
        try {
            serverPort = Integer.parseInt(serverProperties.getProperty("triatlon.server.port"));
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        AbstractServer server = new TriatlonRPCConcurrentServer(serverPort, service);
        try {
            server.start();
        } catch (Exception ex) {
            ex.printStackTrace();
        } finally {
            try {
                server.stop();
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
    }

}
