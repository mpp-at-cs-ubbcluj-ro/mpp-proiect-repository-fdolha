package model.activity;

import model.Entity;

// AthletePoints

public class AthletePoints extends Entity<Integer> {

    // Private Properties

    private Integer athleteId;
    private Integer points;

    // Lifecycle

    public AthletePoints(Integer athleteId) {
        this.athleteId = athleteId;
        this.points = 0;
    }

    public AthletePoints(Integer athleteId, Integer points) {
        this.athleteId = athleteId;
        this.points = points;
    }

    // Getters & Setters

    public Integer getAthleteId() {
        return this.athleteId;
    }

    public Integer getPoints() {
        return this.points;
    }

    public void setAthleteId(Integer athleteId) {
        this.athleteId = athleteId;
    }

    public void setPoints(Integer points) {
        this.points = points;
    }

}
