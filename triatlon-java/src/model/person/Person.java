package model.person;

// Person

import model.Entity;

public class Person extends Entity<Integer> {

    // Private Properties

    private String firstName;
    private String lastName;

    // Lifecycle

    public Person(String firstName, String lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }

    // Getters & Setters

    public String getFirstName() {
        return this.firstName;
    }

    public String getLastName() {
        return this.lastName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    // Override Methods

    @Override
    public String toString() {
        return this.id + ", " + this.firstName + this.lastName;
    }

}
