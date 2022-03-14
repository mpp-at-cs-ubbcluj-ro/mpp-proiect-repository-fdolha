package model;

import java.io.Serializable;

// Entity

public class Entity<ID> implements Serializable {

    // Private Class Properties

    private static final long serialVersionUID = 7331115341259248461L;

    // Protected Properties

    protected ID id;

    // Getters & Setters

    public ID getId() {
        return id;
    }

    public void setId(ID id) {
        this.id = id;
    }

}
