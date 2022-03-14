package model.activity;

import model.Entity;

// AthletePoints

public class AthletePoints extends Entity<Integer> {

    // Private Properties

    private Integer points;

    // Lifecycle

    public AthletePoints(Integer athleteId) {
        this.id = athleteId;
        this.points = 0;
    }

    public AthletePoints(Integer athleteId, Integer points) {
        this.id = athleteId;
        this.points = points;
    }

    // Getters & Setters

    public Integer getAthleteId() {
        return this.id;
    }

    public Integer getPoints() {
        return this.points;
    }

    public void setPoints(Integer points) {
        this.points = points;
    }

}
