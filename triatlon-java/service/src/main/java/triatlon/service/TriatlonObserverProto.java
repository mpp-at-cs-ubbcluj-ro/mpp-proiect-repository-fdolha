package triatlon.service;

import triatlon.model.Result;
import triatlon.proto.Triatlon;

import java.util.List;

public interface TriatlonObserverProto {
    void resultAdded(List<Triatlon.Result> results);
}
