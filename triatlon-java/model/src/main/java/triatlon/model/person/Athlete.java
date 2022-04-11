package triatlon.model.person;

// Athlete

import java.io.Serializable;

public class Athlete extends Person implements Serializable {

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
