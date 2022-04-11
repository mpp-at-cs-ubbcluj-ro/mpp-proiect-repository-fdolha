package triatlon.service;

import triatlon.model.Result;
import triatlon.model.activity.RaceType;
import triatlon.model.person.Athlete;
import triatlon.model.person.Referee;

import java.util.List;

public interface TriatlonServiceInterface {
    public Referee logInReferee(String email, String password, TriatlonObserverInterface observer);
    public void logOutReferee(String email);
    public List<Athlete> getAthletes();
    public List<Result> getAthletesWithTotalPoints();
    public void addResult(Referee referee, Integer athleteId, Integer points);
    public List<Result> getParticipantsWithResultInRace(RaceType raceType);
}
