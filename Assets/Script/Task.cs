using System;
using UnityEngine.Events;

public class Task
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
    private string _description;
    public string Description
    {
        get { return _description; }
        set
        {
            UpdateDescription?.Invoke(value);
            _description = value;
        }
    }
    public UnityEvent<string> UpdateDescription;

    private DateTime _createDate;
    public DateTime CreateDate
    {
        get { return _createDate; }
        set
        {
            UpdateDate?.Invoke(value);
            _createDate = value;
        }
    }
    public UnityEvent<DateTime> UpdateDate;

    private StateTask _state;
    public StateTask State
    {
        get { return _state; }
        set
        {
            UpdateState?.Invoke(value);
            _state = value;
        }
    }
    public UnityEvent<StateTask> UpdateState;

    private Assignee _assignee;
    public Assignee Assignee
    {
        get { return _assignee; }
        set
        {
            UpdateAssignee?.Invoke(value);
            _assignee = value;
        }
    }
    public UnityEvent<Assignee> UpdateAssignee;


    public Task()
    {
        Name = null;
        Description = null;
        CreateDate = DateTime.Now;
        State = StateTask.Open;
        Assignee = new Assignee();
    }
}
