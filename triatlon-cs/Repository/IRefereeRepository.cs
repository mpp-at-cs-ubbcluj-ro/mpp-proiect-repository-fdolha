using Model.Person;

namespace Repository
{
    public interface IRefereeRepository : IRepository<int, Referee>
    {
        Referee FindByEmail(string email);
    }
}