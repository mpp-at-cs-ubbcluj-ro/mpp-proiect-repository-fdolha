namespace triatlon_cs.Model.Person;

// Person

public class Person : Entity<int>
{
    
    // Properties

    private string _firstName;
    private string _lastName;

    public string FirstName
    {
        get => _firstName;
        set => _firstName = value;
    }
    
    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }
    
    // Lifecycle

    public Person(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }
    
    // Override Methods

    public override string ToString()
    {
        return Id + ", " + _firstName + " " + _lastName;
    }
    
}