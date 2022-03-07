namespace triatlon_cs.Model.Person;

// Referee

public class Referee : Person
{

    // Lifecycle

    public Referee(string firstName, string lastName) : base(firstName, lastName) {}

    // Override Methods

    public override string ToString()
    {
        return "Referee | " + base.ToString();
    }

}