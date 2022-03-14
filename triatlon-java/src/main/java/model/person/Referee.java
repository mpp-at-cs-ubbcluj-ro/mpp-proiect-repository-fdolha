package model.person;

// Referee

public class Referee extends Person {

    // Lifecycle

    public Referee(String firstName, String lastName) {
        super(firstName, lastName);
    }

    // Override Methods

    @Override
    public String toString() {
        return "Referee | " + super.toString();
    }

}
