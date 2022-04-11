package triatlon.networking.rpc;

import triatlon.model.Result;
import triatlon.model.activity.RaceType;
import triatlon.model.person.Athlete;
import triatlon.model.person.Referee;
import triatlon.networking.dto.RefereeDTO;
import triatlon.networking.dto.ResultDTO;
import triatlon.service.TriatlonObserverInterface;
import triatlon.service.TriatlonServiceInterface;

import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.List;

public class TriatlonClientRPCWorker implements Runnable, TriatlonObserverInterface {

    private TriatlonServiceInterface server;
    private Socket connection;
    private ObjectInputStream inputStream;
    private ObjectOutputStream outputStream;
    private volatile boolean connected;

    public TriatlonClientRPCWorker(TriatlonServiceInterface server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try {
            outputStream = new ObjectOutputStream(connection.getOutputStream());
            outputStream.flush();
            inputStream = new ObjectInputStream(connection.getInputStream());
            connected = true;
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    @Override
    public void run() {
        while (connected) {
            try {
                Object request = inputStream.readObject();
                Response response = handleRequest((Request) request);
                sendResponse(response);
            } catch (Exception ex) {
                ex.printStackTrace();
            }
            try {
                Thread.sleep(1000);
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
        try {
            inputStream.close();
            outputStream.close();
            connection.close();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    @Override
    public void resultAdded(List<Result> results) {
        Response response = new Response.Builder().type(ResponseType.RESULT_ADDED).data(results).build();
        try {
            sendResponse(response);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private static final Response OK = new Response.Builder().type(ResponseType.OK).build();
    private static final Response ERROR = new Response.Builder().type(ResponseType.ERROR).build();

    private Response handleRequest(Request request) {
        if (request.type() == RequestType.LOGIN) {
            RefereeDTO refereeDTO = (RefereeDTO) request.data();
            Referee referee = server.logInReferee(refereeDTO.getEmail(), refereeDTO.getPassword(), this);
            if (referee != null) {
                RefereeDTO refDTO = new RefereeDTO(referee.getFirstName(), referee.getLastName(), referee.getRaceType(), referee.getEmail(), referee.getPassword());
                return new Response.Builder().type(ResponseType.OK).data(refDTO).build();
            }
            else {
                connected = false;
                return ERROR;
            }
        }
        if (request.type() == RequestType.LOGOUT) {
            String email = (String) request.data();
            server.logOutReferee(email);
            return OK;
        }
        if (request.type() == RequestType.ATHLETES) {
            List<Athlete> athletes = server.getAthletes();
            return new Response.Builder().type(ResponseType.ATHLETES).data(athletes).build();
        }
        if (request.type() == RequestType.ATHLETES_WITH_TOTAL_POINTS) {
            List<Result> results = server.getAthletesWithTotalPoints();
            return new Response.Builder().type(ResponseType.ATHLETES_WITH_POINTS).data(results).build();
        }
        if (request.type() == RequestType.ADD_RESULT) {
            ResultDTO resultDTO = (ResultDTO) request.data();
            server.addResult(resultDTO.getReferee(), resultDTO.getAthleteId(), resultDTO.getPoints());
            return OK;
        }
        if (request.type() == RequestType.PARTICIPANTS_WITH_RESULT_IN_RACE) {
            RaceType raceType = (RaceType) request.data();
            List<Result> results = server.getParticipantsWithResultInRace(raceType);
            return new Response.Builder().type(ResponseType.PARTICIPANTS_WITH_RESULT_IN_RACE).data(results).build();
        }
        return OK;
    }

    private void sendResponse(Response response) {
        try {
            outputStream.writeObject(response);
            outputStream.flush();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

}
