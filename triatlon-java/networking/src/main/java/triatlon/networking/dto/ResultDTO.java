package triatlon.networking.dto;

import triatlon.model.person.Referee;

import java.io.Serializable;

public class ResultDTO implements Serializable {

    private Referee referee;
    private Integer athleteId;
    private Integer points;

    public ResultDTO(Referee referee, Integer athleteId, Integer points) {
        this.referee = referee;
        this.athleteId = athleteId;
        this.points = points;
    }

    public ResultDTO(Integer athleteId, Integer points) {
        this.athleteId = athleteId;
        this.points = points;
    }

    public Referee getReferee() {
        return referee;
    }

    public void setReferee(Referee referee) {
        this.referee = referee;
    }

    public Integer getAthleteId() {
        return athleteId;
    }

    public void setAthleteId(Integer athleteId) {
        this.athleteId = athleteId;
    }

    public Integer getPoints() {
        return points;
    }

    public void setPoints(Integer points) {
        this.points = points;
    }

}
