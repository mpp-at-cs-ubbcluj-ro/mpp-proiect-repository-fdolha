

// Referee

public class Referee : Person
{

    // Properties
    
    private RaceType _raceType;
    private string _email;
    private string _password;

    public RaceType RaceType
    {
        get => _raceType;
    }

    public string Email
    {
        get => _email;
    }

    public string Password
    {
        get => _password;
    }

    // Lifecycle

    public Referee(string firstName, string lastName, RaceType raceType, string email, string password) : base(
        firstName, lastName)
    {
        _raceType = raceType;
        _email = email;
        _password = password;
    }

    // Override Methods

    public override string ToString()
    {
        return "Referee | " + base.ToString();
    }

}