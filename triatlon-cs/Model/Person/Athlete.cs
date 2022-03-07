namespace triatlon_cs.Model.Person;

// Athlete

public class Athlete : Person
{
    
    // Lifecycle

    public Athlete(string firstName, string lastName) : base(firstName, lastName) {}
    
    // Override Methods

    public override string ToString()
    {
        return "Athlete | " + base.ToString();
    }
    
}