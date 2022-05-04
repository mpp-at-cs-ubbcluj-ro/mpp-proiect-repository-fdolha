package triatlon.client;

import triatlon.networking.proto.TriatlonServiceProtoRPCProxy;
import triatlon.proto.Triatlon;
import triatlon.service.TriatlonServiceProto;

import java.util.Properties;

public class DependencyProvider {

    private static DependencyProvider instance = null;
    private static final int defaultPort = 55555;
    private static final String defaultServer = "localhost";

    private TriatlonServiceProto server;
    private Triatlon.Referee referee;

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
        server = new TriatlonServiceProtoRPCProxy(serverIP, serverPort);
    }

    public TriatlonServiceProto getServer() {
        return server;
    }

    public static DependencyProvider getInstance() {
        if (instance == null) instance = new DependencyProvider();
        return instance;
    }

    public void setReferee(Triatlon.Referee referee) {
        this.referee = referee;
    }

    public Triatlon.Referee getReferee() {
        return this.referee;
    }

}
