package ro.ubbcluj.scs.dfar2166.triatlonjava.model.activity;

import ro.ubbcluj.scs.dfar2166.triatlonjava.model.Entity;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.Optional;

// Race

public class Race extends Entity<Integer> {

    // Private Properties

    private final RaceType type;
    private final List<AthletePoints> athletesPoints;

    // Lifecycle

    public Race(RaceType type) {
        this.type = type;
        this.athletesPoints = new ArrayList<>();
    }

    // Private Methods

    private Optional<AthletePoints> getAthletePoints(Integer athleteId) {
        return athletesPoints.stream().filter(athletePoints -> Objects.equals(athletePoints.getAthleteId(), athleteId)).findFirst();
    }

    private Boolean athletesPointsContainsAthlete(Integer athleteId) {
        return getAthletePoints(athleteId).isPresent();
    }

    // Instance Methods

    public void addAthlete(Integer athleteId) {
        if (athletesPointsContainsAthlete(athleteId)) { return; }
        this.athletesPoints.add(new AthletePoints(athleteId));
    }

    public void addAthlete(Integer athleteId, Integer points) {
        if (athletesPointsContainsAthlete(athleteId)) {
            setPointsForAthlete(athleteId, points);
            return;
        }
        this.athletesPoints.add(new AthletePoints(athleteId, points));
    }

    public void setPointsForAthlete(Integer athleteId, Integer points) {
        Optional<AthletePoints> entity = getAthletePoints(athleteId);
        entity.ifPresent(athletePoints -> athletePoints.setPoints(points));
    }

    public Optional<Integer> getPointsForAthlete(Integer athleteId) {
        return Optional.ofNullable(getAthletePoints(athleteId).get().getPoints());
    }

    // Getters & Setters

    public RaceType getType() {
        return type;
    }

    public List<AthletePoints> getAthletesPoints() {
        return this.athletesPoints;
    }

}
