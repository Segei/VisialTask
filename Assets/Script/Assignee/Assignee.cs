using System;
using UnityEngine;
using UnityEngine.Events;


namespace Script.Assignee
{
    [Serializable]
    public class Assignee
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
        [SerializeField] private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                UpdateLastName?.Invoke(value);
            }
        }
        [NonSerialized]
        public UnityEvent<string> UpdateLastName = new UnityEvent<string>();


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
}