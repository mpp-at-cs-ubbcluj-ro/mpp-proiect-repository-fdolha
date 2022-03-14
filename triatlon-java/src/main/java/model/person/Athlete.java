package model.person;

// Athlete

public class Athlete extends Person {

    // Lifecycle

    public Athlete(String firstName, String lastName) {
        super(firstName, lastName);
    }

    // Override Methods

    @Override
    public String toString() {
        return "Athlete | " + super.toString();
    }

}
