package triatlon.networking.utils;

import java.net.ServerSocket;
import java.net.Socket;

public abstract class AbstractServer {

    private int port;
    private ServerSocket server = null;

    public AbstractServer(int port) {
        this.port = port;
    }

    public void start() {
        try {
            server = new ServerSocket(port);
            while (true) {
                Socket client = server.accept();
                processRequest(client);
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        } finally {
            stop();
        }
    }

    protected abstract void processRequest(Socket client);

    public void stop() {
        try {
            server.close();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

}
