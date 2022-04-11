package triatlon.model;

// AthleteResult

import java.io.Serializable;

public class Result implements Serializable {

    // Private Properties

    private Integer id;
    private String name;
    private Integer points;
    private final String reversedName;

    // Lifecycle

    public Result(Integer id, String name, String reversedName, Integer points) {
        this.id = id;
        this.name = name;
        this.reversedName = reversedName;
        this.points = points;
    }

    // Getters & Setters

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public String getNameReversed() {
        return this.reversedName;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getPoints() {
        return points;
    }

    public void setPoints(Integer points) {
        this.points = points;
    }

}
