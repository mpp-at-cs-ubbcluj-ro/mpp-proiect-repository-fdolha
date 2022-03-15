package model.activity;

// RaceType

public enum RaceType {
    SWIMMING(1), CYCLING(2), RUNNING(3);

    private final int value;
    RaceType(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }

    public static RaceType fromValue(int value) {
        for (var raceType : RaceType.values()) {
            if (raceType.getValue() == value) {
                return raceType;
            }
        }
        return null;
    }
}
