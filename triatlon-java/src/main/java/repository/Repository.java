package repository;

import model.Entity;

// Repository

public interface Repository<ID, E extends Entity<ID>> {

    E findOne(ID id);
    Iterable<E> findAll();
    E save(E entity);
    ID delete(ID id);
    E update(E entity);

}
