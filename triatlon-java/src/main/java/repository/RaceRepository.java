package repository;

import model.activity.AthletePoints;
import model.activity.Race;
import model.activity.RaceType;

// RaceRepository

public abstract class RaceRepository implements Repository<Integer, AthletePoints> {

    // Private Properties

    private RaceType raceType;

    // Lifecycle

    public RaceRepository(RaceType raceType) {
        this.raceType = raceType;
    }

    public RaceRepository(Race race) {
        this.raceType = race.getType();
    }

    // Getters & Setters

    public RaceType getRaceType() {
        return this.raceType;
    }

    public void setRaceType(RaceType raceType) {
        this.raceType = raceType;
    }

}
