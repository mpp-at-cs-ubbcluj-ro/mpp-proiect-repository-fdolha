using triatlon_cs.Model.Person;

namespace triatlon_cs.Repository;

// IRefereeRepository

public interface IRefereeRepository : IRepository<int, Referee>
{
    Referee FindByEmail(string email);
}