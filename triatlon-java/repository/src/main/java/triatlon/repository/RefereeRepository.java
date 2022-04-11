package triatlon.repository;

import triatlon.model.person.Referee;

public interface RefereeRepository extends Repository<Integer, Referee> {

    Referee findByEmail(String email);

}
