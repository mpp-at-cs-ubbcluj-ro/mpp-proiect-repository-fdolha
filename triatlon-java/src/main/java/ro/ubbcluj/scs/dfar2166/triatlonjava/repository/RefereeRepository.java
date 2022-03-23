package ro.ubbcluj.scs.dfar2166.triatlonjava.repository;

import ro.ubbcluj.scs.dfar2166.triatlonjava.model.person.Referee;

public interface RefereeRepository extends Repository<Integer, Referee> {

    Referee findByEmail(String email);

}
