package ro.ubbcluj.scs.dfar2166.triatlonjava.service;



import ro.ubbcluj.scs.dfar2166.triatlonjava.model.Result;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.activity.RaceType;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.activity.AthletePoints;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.person.Athlete;
import ro.ubbcluj.scs.dfar2166.triatlonjava.model.person.Referee;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.AthleteRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.RaceRepository;
import ro.ubbcluj.scs.dfar2166.triatlonjava.repository.RefereeRepository;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;

// TriatlonService

public class TriatlonService {

    // Private Properties

    private final RaceRepository raceRepository;
    private final RefereeRepository refereeRepository;
    private final AthleteRepository athleteRepository;
    private Referee loggedInReferee = null;

    // Lifecycle

    public TriatlonService(RaceRepository raceRepository, RefereeRepository refereeRepository, AthleteRepository athleteRepository) {
        this.raceRepository = raceRepository;
        this.refereeRepository = refereeRepository;
        this.athleteRepository = athleteRepository;
    }

    // Instance Methods

    public boolean logInReferee(String email, String password) {
        Referee referee = this.refereeRepository.findByEmail(email);
        if (referee == null) return false;
        var isLoggedIn = referee.getEmail().equals(email) && referee.getPassword().equals(password);
        if (isLoggedIn) {
            loggedInReferee = referee;
            this.raceRepository.setRaceType(referee.getRaceType());
        }
        return isLoggedIn;
    }

    public void logOutReferee() {
        loggedInReferee = null;
        this.raceRepository.setRaceType(null);
    }

    public Referee getCurrentReferee() {
        return loggedInReferee;
    }

    public List<Athlete> getAthletes() {
        List<Athlete> athletes = new ArrayList<>();
        this.athleteRepository.findAll().forEach(athletes::add);
        return athletes;
    }

    public List<Result> getAthletesWithTotalPoints() {
        List<Result> athletesWithResults = new ArrayList<>();
        this.athleteRepository.findAll().forEach(athlete -> {
            var points = 0;
            for (var value : RaceType.values()) {
                raceRepository.setRaceType(value);
                AthletePoints athletePoints = raceRepository.findByAthleteId(athlete.getId());
                if (athletePoints != null) points += athletePoints.getPoints();
            }
            Result athleteResult = new Result(athlete.getId(), athlete.getFullName(), athlete.getFullNameReversed(), points);
            athletesWithResults.add(athleteResult);
        });
        raceRepository.setRaceType(loggedInReferee.getRaceType());
        return athletesWithResults.stream().sorted(Comparator.comparing(Result::getNameReversed)).collect(Collectors.toList());
    }

    public void addResult(Integer athleteId, Integer points) {
        AthletePoints athletePoints = this.raceRepository.findByAthleteId(athleteId);
        if (athletePoints == null) {
            athletePoints = new AthletePoints(athleteId, points);
            this.raceRepository.save(athletePoints);
        } else {
            athletePoints.setPoints(points);
            this.raceRepository.update(athletePoints);
        }
    }

    public List<Result> getParticipantsWithResultInRace() {
        List<Result> results = new ArrayList<>();
        this.raceRepository.findAllWithPoints().forEach(athletePoints -> {
            Athlete athlete = this.athleteRepository.findOne(athletePoints.getAthleteId());
            Result result = new Result(athlete.getId(), athlete.getFullName(), athlete.getFullNameReversed(), athletePoints.getPoints());
            results.add(result);
        });
        return results.stream().sorted(Comparator.comparing(Result::getPoints).reversed()).collect(Collectors.toList());
    }

}
