package triatlon.networking.proto;

import triatlon.proto.Triatlon;
import triatlon.service.TriatlonObserverProto;
import triatlon.service.TriatlonServiceProto;

import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class TriatlonServiceProtoRPCProxy implements TriatlonServiceProto {

    private final String host;
    private final int port;
    private TriatlonObserverProto client;
    private InputStream inputStream;
    private OutputStream outputStream;
    private Socket connection;
    private final BlockingQueue<Triatlon.Response> responses;
    private volatile boolean finished;

    public TriatlonServiceProtoRPCProxy(String host, int port) {
        this.host = host;
        this.port = port;
        responses = new LinkedBlockingQueue<>();
    }

    private void initConnection() {
        try {
            connection = new Socket(host, port);
            outputStream = connection.getOutputStream();
            inputStream = connection.getInputStream();
            finished = false;
            startReader();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private void closeConnection() {
        finished = true;
        try {
            inputStream.close();
            outputStream.close();
            connection.close();
            client = null;
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private void sendRequest(Triatlon.Request request) {
        try {
            request.writeDelimitedTo(outputStream);
            outputStream.flush();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private Triatlon.Response readResponse() {
        Triatlon.Response response = null;
        try {
            response = responses.take();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return response;
    }

    private void startReader() {
        Thread thread = new Thread(new TriatlonServiceProtoRPCProxy.ReaderThread());
        thread.start();
    }

    private boolean isUpdate(Triatlon.Response response) {
        return response.getType() == Triatlon.Response.Type.RESULT_ADDED;
    }

    private void handleUpdate(Triatlon.Response response) {
        if (response.getType() == Triatlon.Response.Type.RESULT_ADDED) {
            client.resultAdded(response.getResultsList());
        }
    }

    @Override
    public Triatlon.Referee logInReferee(String email, String password, TriatlonObserverProto observer) {
        initConnection();
        Triatlon.Referee refereeDTO = Triatlon.Referee.newBuilder().setEmail(email).setPassword(password).build();
        Triatlon.Request request = Triatlon.Request.newBuilder().setType(Triatlon.Request.Type.LOGIN).setReferee(refereeDTO).build();
        sendRequest(request);
        Triatlon.Response response = readResponse();
        Triatlon.Referee referee = null;
        if (response.getType() == Triatlon.Response.Type.OK) {
            referee = response.getReferee();
            this.client = observer;
        } else { closeConnection(); }
        return referee;
    }

    @Override
    public void logOutReferee(String email) {
        initConnection();
        Triatlon.Request request = Triatlon.Request.newBuilder().setType(Triatlon.Request.Type.LOGOUT).setEmail(email).build();
        sendRequest(request);
        closeConnection();
    }

    @Override
    public List<Triatlon.Athlete> getAthletes() {
        Triatlon.Request request = Triatlon.Request.newBuilder().setType(Triatlon.Request.Type.ATHLETES).build();
        sendRequest(request);
        Triatlon.Response response = readResponse();
        return response.getAthletesList();
    }

    @Override
    public List<Triatlon.Result> getAthletesWithTotalPoints() {
        if (connection == null) { initConnection(); }
        Triatlon.Request request = Triatlon.Request.newBuilder().setType(Triatlon.Request.Type.ATHLETES_WITH_TOTAL_POINTS).build();
        sendRequest(request);
        Triatlon.Response response = readResponse();
        return response.getResultsList();
    }

    @Override
    public void addResult(Triatlon.Referee referee, Integer athleteId, Integer points) {
        Triatlon.ResultDTO resultDTO = Triatlon.ResultDTO.newBuilder().setReferee(referee).setAthleteId(athleteId).setPoints(points).build();
        Triatlon.Request request = Triatlon.Request.newBuilder().setType(Triatlon.Request.Type.ADD_RESULT).setResultDTO(resultDTO).build();
        sendRequest(request);
        readResponse();
    }

    @Override
    public List<Triatlon.Result> getParticipantsWithResultInRace(Triatlon.RaceType raceType) {
        Triatlon.Request request = Triatlon.Request.newBuilder().setType(Triatlon.Request.Type.PARTICIPANTS_WITH_RESULT_IN_RACE).setRaceType(raceType).build();
        sendRequest(request);
        Triatlon.Response response = readResponse();
        return response.getResultsList();
    }

    private class ReaderThread implements Runnable {
        @Override
        public void run() {
            while (!finished) {
                try {
                    Triatlon.Response response = Triatlon.Response.parseDelimitedFrom(inputStream);
                    if (isUpdate(response)) {
                        handleUpdate(response);
                    } else {
                        try {
                            responses.put(response);
                        } catch (Exception ex) {
                            ex.printStackTrace();
                        }
                    }
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        }
    }

}
