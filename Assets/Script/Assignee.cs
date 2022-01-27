using UnityEngine.Events;

public class Assignee
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set
        {
            UpdateName?.Invoke(value);
            _name = value;
        }
    }
    public UnityEvent<string> UpdateName;

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set
        {
            UpdateLastName?.Invoke(value);
            _lastName = value;
        }
    }
    public UnityEvent<string> UpdateLastName;

    public override string ToString()
    {
        return Name + " " + LastName;
    }
    public Assignee()
    {
        Name = null;
        LastName = null;
    }
}
