package triatlon.repository;

import triatlon.model.activity.AthletePoints;
import triatlon.model.activity.Race;
import triatlon.model.activity.RaceType;

import java.util.List;

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

    // Abstract Methods

    public abstract AthletePoints findByAthleteId(Integer athleteId);
    public abstract Iterable<AthletePoints> findAllWithPoints();

    // Getters & Setters

    public RaceType getRaceType() {
        return this.raceType;
    }

    public void setRaceType(RaceType raceType) {
        this.raceType = raceType;
    }

}
