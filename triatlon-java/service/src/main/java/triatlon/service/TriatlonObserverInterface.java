package triatlon.service;

import triatlon.model.Result;

import java.util.List;

public interface TriatlonObserverInterface {
    void resultAdded(List<Result> results);
}
