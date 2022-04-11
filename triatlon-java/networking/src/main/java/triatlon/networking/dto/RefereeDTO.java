package triatlon.networking.dto;

import triatlon.model.activity.RaceType;

import java.io.Serializable;

public class RefereeDTO implements Serializable {

    private String firstName;
    private String lastName;
    private RaceType raceType;
    private String email;
    private String password;

    public RefereeDTO(String email, String password) {
        this.email = email;
        this.password = password;
    }

    public RefereeDTO(String firstName, String lastName, RaceType raceType, String email, String password) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.raceType = raceType;
        this.email = email;
        this.password = password;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public RaceType getRaceType() {
        return raceType;
    }

    public void setRaceType(RaceType raceType) {
        this.raceType = raceType;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
