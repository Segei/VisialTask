using System;
using UnityEngine;
using UnityEngine.Events;


namespace Script.Task
{
    public class Task
    {
        [SerializeField] private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                UpdateName?.Invoke(value);
            }

        }
        [NonSerialized]
        public UnityEvent<string> UpdateName = new UnityEvent<string>();
        [SerializeField] private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                UpdateDescription?.Invoke(value);
            }
        }
        [NonSerialized]
        public UnityEvent<string> UpdateDescription = new UnityEvent<string>();
        [SerializeField] private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set
            {
                _createDate = value;
                UpdateDate?.Invoke(value);
            }
        }
        [NonSerialized]
        public UnityEvent<DateTime> UpdateDate = new UnityEvent<DateTime>();
        [SerializeField] private StateTask _state;
        public StateTask State
        {
            get { return _state; }
            set
            {
                _state = value;
                UpdateState?.Invoke((int)value);
            }
        }
        [NonSerialized]
        public UnityEvent<int> UpdateState = new UnityEvent<int>();
        [SerializeField] private int _assignee;
        public int Assignee
        {
            get { return _assignee; }
            set
            {
                _assignee = value;
                UpdateAssignee?.Invoke(value);
            }
        }
        [NonSerialized]
        public UnityEvent<int> UpdateAssignee = new UnityEvent<int>();


        public Task()
        {
            Name = null;
            Description = null;
            CreateDate = DateTime.Now;
            State = StateTask.Open;
            Assignee = 0;
        }
    }
}