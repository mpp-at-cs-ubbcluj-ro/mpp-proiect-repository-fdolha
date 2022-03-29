namespace triatlon_cs.Model;

public class Result
{
    
    // Properties

    private int _id;
    private string _name;
    private int _points;
    private string _reversedName;

    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    public string Name
    {
        get => _name;
        set => _name = value;
    }
    
    public int Points
    {
        get => _points;
        set => _points = value;
    }
    
    public string ReversedName
    {
        get => _reversedName;
        set => _reversedName = value;
    }
    
    // Lifecycle

    public Result(int id, string name, string reversedName, int points)
    {
        this._id = id;
        this._points = points;
        this._reversedName = reversedName;
        this._name = name;
    }

}