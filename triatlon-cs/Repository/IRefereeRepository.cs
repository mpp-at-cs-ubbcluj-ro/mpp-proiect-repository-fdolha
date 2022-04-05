
// IRefereeRepository

public interface IRefereeRepository : IRepository<int, Referee>
{
    Referee FindByEmail(string email);
}