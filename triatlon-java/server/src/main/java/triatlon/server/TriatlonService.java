package triatlon.server;

import triatlon.model.Result;
import triatlon.model.activity.AthletePoints;
import triatlon.model.activity.RaceType;
import triatlon.model.person.Athlete;
import triatlon.model.person.Referee;
import triatlon.repository.AthleteRepository;
import triatlon.repository.RaceRepository;
import triatlon.repository.RefereeRepository;
import triatlon.service.TriatlonObserverInterface;
import triatlon.service.TriatlonServiceInterface;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.stream.Collectors;

public class TriatlonService implements TriatlonServiceInterface  {

    private final RaceRepository raceRepository;
    private final RefereeRepository refereeRepository;
    private final AthleteRepository athleteRepository;
    private Map<String, TriatlonObserverInterface> loggedReferees;

    public TriatlonService(RaceRepository raceRepository, RefereeRepository refereeRepository, AthleteRepository athleteRepository) {
        this.raceRepository = raceRepository;
        this.refereeRepository = refereeRepository;
        this.athleteRepository = athleteRepository;
        loggedReferees = new ConcurrentHashMap<>();
    }

    @Override
    public synchronized Referee logInReferee(String email, String password, TriatlonObserverInterface observer) {
        Referee referee = this.refereeRepository.findByEmail(email);
        if (referee == null) return null;
        var isLoggedIn = referee.getEmail().equals(email) && referee.getPassword().equals(password);
        if (isLoggedIn) {
            loggedReferees.putIfAbsent(referee.getEmail(), observer);
        }
        return referee;
    }

    @Override
    public synchronized void logOutReferee(String email) {
        loggedReferees.remove(email);
    }

    @Override
    public synchronized List<Athlete> getAthletes() {
        List<Athlete> athletes = new ArrayList<>();
        this.athleteRepository.findAll().forEach(athletes::add);
        return athletes;
    }

    @Override
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
        return athletesWithResults.stream().sorted(Comparator.comparing(Result::getNameReversed)).collect(Collectors.toList());
    }

    @Override
    public void addResult(Referee referee, Integer athleteId, Integer points) {
        this.raceRepository.setRaceType(referee.getRaceType());
        AthletePoints athletePoints = this.raceRepository.findByAthleteId(athleteId);
        if (athletePoints == null) {
            athletePoints = new AthletePoints(athleteId, points);
            this.raceRepository.save(athletePoints);
        } else {
            athletePoints.setPoints(points);
            this.raceRepository.update(athletePoints);
        }
        ExecutorService executor = Executors.newFixedThreadPool(5);
        loggedReferees.forEach((email, observer) -> {
            executor.execute(() -> {
                try {
                    Thread.sleep(1000);
                    observer.resultAdded(getAthletesWithTotalPoints());
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            });
        });
        executor.shutdown();
    }

    @Override
    public List<Result> getParticipantsWithResultInRace(RaceType raceType) {
        this.raceRepository.setRaceType(raceType);
        List<Result> results = new ArrayList<>();
        this.raceRepository.findAllWithPoints().forEach(athletePoints -> {
            Athlete athlete = this.athleteRepository.findOne(athletePoints.getAthleteId());
            Result result = new Result(athlete.getId(), athlete.getFullName(), athlete.getFullNameReversed(), athletePoints.getPoints());
            results.add(result);
        });
        return results.stream().sorted(Comparator.comparing(Result::getPoints).reversed()).collect(Collectors.toList());
    }

}
