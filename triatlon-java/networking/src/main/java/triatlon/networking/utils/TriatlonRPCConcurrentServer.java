package triatlon.networking.utils;

import triatlon.networking.rpc.TriatlonClientRPCWorker;
import triatlon.service.TriatlonServiceInterface;

import java.net.Socket;

public class TriatlonRPCConcurrentServer extends AbstractConcurrentServer {

    private TriatlonServiceInterface triatlonServer;

    public TriatlonRPCConcurrentServer(int port, TriatlonServiceInterface triatlonServer) {
        super(port);
        this.triatlonServer = triatlonServer;
    }

    @Override
    protected Thread createWorker(Socket client) {
        TriatlonClientRPCWorker worker = new TriatlonClientRPCWorker(triatlonServer, client);
        return new Thread(worker);
    }

}
