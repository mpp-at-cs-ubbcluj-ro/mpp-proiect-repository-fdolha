package ro.ubbcluj.scs.dfar2166.triatlonjava.repository;

import ro.ubbcluj.scs.dfar2166.triatlonjava.model.Entity;

// Repository

public interface Repository<ID, E extends Entity<ID>> {

    E findOne(ID id);
    Iterable<E> findAll();
    E save(E entity);
    ID delete(ID id);
    E update(E entity);

}
