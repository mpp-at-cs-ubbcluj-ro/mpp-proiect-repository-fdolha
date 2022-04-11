package triatlon.networking.rpc;

import triatlon.model.Result;
import triatlon.model.activity.RaceType;
import triatlon.model.person.Athlete;
import triatlon.model.person.Referee;
import triatlon.networking.dto.RefereeDTO;
import triatlon.networking.dto.ResultDTO;
import triatlon.service.TriatlonObserverInterface;
import triatlon.service.TriatlonServiceInterface;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingDeque;
import java.util.stream.Collectors;

public class TriatlonServiceRPCProxy implements TriatlonServiceInterface {

    private String host;
    private int port;
    private TriatlonObserverInterface client;
    private ObjectInputStream inputStream;
    private ObjectOutputStream outputStream;
    private Socket connection;
    private BlockingQueue<Response> responses;
    private volatile boolean finished;

    public TriatlonServiceRPCProxy(String host, int port) {
        this.host = host;
        this.port = port;
        responses = new LinkedBlockingDeque<Response>();
    }

    private void initConnection() {
        try {
            connection = new Socket(host, port);
            outputStream = new ObjectOutputStream(connection.getOutputStream());
            outputStream.flush();
            inputStream = new ObjectInputStream(connection.getInputStream());
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
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

    private void sendRequest(Request request) {
        try {
            outputStream.writeObject(request);
            outputStream.flush();
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    private Response readResponse() {
        Response response = null;
        try {
            response = responses.take();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return response;
    }

    private void startReader() {
        Thread thread = new Thread(new ReaderThread());
        thread.start();
    }

    private boolean isUpdate(Response response) {
        return response.type() == ResponseType.RESULT_ADDED;
    }

    private void handleUpdate(Response response) {
        if (response.type() == ResponseType.RESULT_ADDED) {
            client.resultAdded((List<Result>) response.data());
        }
    }

    private class ReaderThread implements Runnable {
        @Override
        public void run() {
            while (!finished) {
                try {
                    Object response = inputStream.readObject();
                    if (isUpdate((Response) response)) {
                        handleUpdate((Response) response);
                    } else {
                        try {
                            responses.put((Response) response);
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

    @Override
    public Referee logInReferee(String email, String password, TriatlonObserverInterface observer) {
        initConnection();
        RefereeDTO refereeDTO = new RefereeDTO(email, password);
        Request request = new Request.Builder().type(RequestType.LOGIN).data(refereeDTO).build();
        sendRequest(request);
        Response response = readResponse();
        Referee referee = null;
        if (response.type() == ResponseType.OK) {
            var refDTO = (RefereeDTO) response.data();
            referee = new Referee(refDTO.getFirstName(), refDTO.getLastName(), refDTO.getRaceType(), refDTO.getEmail(), refDTO.getPassword());
            this.client = observer;
        } else { closeConnection(); }
        return referee;
    }

    @Override
    public void logOutReferee(String email) {
        initConnection();
        Request request = new Request.Builder().type(RequestType.LOGOUT).data(email).build();
        sendRequest(request);
        closeConnection();
    }

    @Override
    public List<Athlete> getAthletes() {
        Request request = new Request.Builder().type(RequestType.ATHLETES).build();
        sendRequest(request);
        Response response = readResponse();
        return (ArrayList<Athlete>) response.data();
    }

    @Override
    public List<Result> getAthletesWithTotalPoints() {
        if (connection == null) {
            initConnection();
        }
        Request request = new Request.Builder().type(RequestType.ATHLETES_WITH_TOTAL_POINTS).build();
        sendRequest(request);
        Response response = readResponse();
        return (ArrayList<Result>) response.data();
    }

    @Override
    public void addResult(Referee referee, Integer athleteId, Integer points) {
        ResultDTO resultDTO = new ResultDTO(referee, athleteId, points);
        Request request = new Request.Builder().type(RequestType.ADD_RESULT).data(resultDTO).build();
        sendRequest(request);
        readResponse();
    }

    @Override
    public List<Result> getParticipantsWithResultInRace(RaceType raceType) {
        Request request = new Request.Builder().type(RequestType.PARTICIPANTS_WITH_RESULT_IN_RACE).build();
        sendRequest(request);
        Response response = readResponse();
        var resultsDTO = (Result[]) response.data();
        return Arrays.stream(resultsDTO).collect(Collectors.toList());
    }
}
