package triatlon.service;

import triatlon.proto.Triatlon;

import java.util.List;

public interface TriatlonServiceProto {
    Triatlon.Referee logInReferee(String email, String password, TriatlonObserverProto observer);
    void logOutReferee(String email);
    List<Triatlon.Athlete> getAthletes();
    List<Triatlon.Result> getAthletesWithTotalPoints();
    void addResult(Triatlon.Referee referee, Integer athleteId, Integer points);
    List<Triatlon.Result> getParticipantsWithResultInRace(Triatlon.RaceType raceType);
}
