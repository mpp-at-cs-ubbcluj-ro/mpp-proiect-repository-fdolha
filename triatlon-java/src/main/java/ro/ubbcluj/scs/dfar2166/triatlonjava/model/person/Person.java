package ro.ubbcluj.scs.dfar2166.triatlonjava.model.person;

import ro.ubbcluj.scs.dfar2166.triatlonjava.model.Entity;

// Person

public class Person extends Entity<Integer> implements Comparable<Person> {

    // Private Properties

    private String firstName;
    private String lastName;

    // Lifecycle

    protected Person(String firstName, String lastName) {
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

    public String getFullName() {
        return this.firstName + " " + this.lastName;
    }

    public String getFullNameReversed() {
        return this.lastName + " " + this.lastName;
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
        return this.id + ", " + this.firstName + " " + this.lastName;
    }

    @Override
    public int compareTo(Person o) {
        return this.lastName.compareTo(o.lastName);
    }
}
