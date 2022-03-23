package ro.ubbcluj.scs.dfar2166.triatlonjava.model.person;

import ro.ubbcluj.scs.dfar2166.triatlonjava.model.activity.RaceType;

// Referee

public class Referee extends Person {

    // Private Properties

    private final RaceType raceType;
    private final String email;
    private final String password;

    // Lifecycle

    public Referee(String firstName, String lastName, RaceType raceType, String email, String password) {
        super(firstName, lastName);
        this.raceType = raceType;
        this.email = email;
        this.password = password;
    }

    // Getters

    public RaceType getRaceType() {
        return raceType;
    }

    public String getEmail() {
        return email;
    }

    public String getPassword() {
        return password;
    }

    // Override Methods

    @Override
    public String toString() {
        return "Referee | " + super.toString();
    }

}
