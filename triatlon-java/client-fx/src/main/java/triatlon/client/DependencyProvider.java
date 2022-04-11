package triatlon.client;

import triatlon.model.person.Referee;
import triatlon.networking.rpc.TriatlonServiceRPCProxy;
import triatlon.service.TriatlonServiceInterface;

import java.sql.Ref;
import java.util.Properties;

public class DependencyProvider {

    private static DependencyProvider instance = null;
    private static final int defaultPort = 55555;
    private static final String defaultServer = "localhost";

    private TriatlonServiceInterface server;
    private Referee referee;

    private DependencyProvider() {
        Properties clientProperties = new Properties();
        try {
            clientProperties.load(TriatlonApplication.class.getResourceAsStream("/triatlonclient.properties"));
        } catch (Exception ex) {
            ex.printStackTrace();
            return;
        }
        String serverIP = clientProperties.getProperty("triatlon.server.host", defaultServer);
        int serverPort = defaultPort;
        try {
            serverPort = Integer.parseInt(clientProperties.getProperty("triatlon.server.port"));
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        server = new TriatlonServiceRPCProxy(serverIP, serverPort);
    }

    public TriatlonServiceInterface getServer() {
        return server;
    }

    public static DependencyProvider getInstance() {
        if (instance == null) instance = new DependencyProvider();
        return instance;
    }

    public void setReferee(Referee referee) {
        this.referee = referee;
    }

    public Referee getReferee() {
        return this.referee;
    }

}
