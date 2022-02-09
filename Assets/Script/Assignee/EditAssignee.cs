using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Script.Assignee
{
    public class EditAssignee : MonoBehaviour
    {
        private Assignee _assignee;
        public Assignee Assignee
        {
            get { return _assignee; }
            set
            {
                _assignee = value;
                _name.Initialized("Name");
                _name.SetInput(_assignee.Name);
                _name.InputField.onValueChanged.AddListener((input) => _assignee.Name = input);
                _lastName.Initialized("LastName");
                _lastName.SetInput(_assignee.LastName);
                _lastName.InputField.onValueChanged.AddListener((input) => _assignee.LastName = input);
                _removeMe.onClick.RemoveAllListeners();
                _removeMe.onClick.AddListener(Remove);
            }
        }
        [SerializeField] private Button _removeMe;
        [SerializeField] private InputPrefab _name;
        [SerializeField] private InputPrefab _lastName;
        public UnityEvent<EditAssignee> ImRemove;

        private void Remove()
        {
            ImRemove?.Invoke(this);
        }
    }
}