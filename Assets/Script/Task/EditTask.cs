using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Script.Task
{
    public class EditTask : MonoBehaviour, IViewEditTask
    {
        private Task _task;
        public Task Task
        {
            get { return _task; }
            set
            {
                _task = value;
                _name.Initialized("Name");
                _name.InputField.onValueChanged.AddListener((input) => _task.Name = input);
                _name.SetInput(_task.Name);
                _discription.Initialized("Discription");
                _discription.InputField.onValueChanged.AddListener((input) => _task.Description = input);
                _discription.SetInput(_task.Description);
                _date.text = _task.CreateDate.ToString("MMM dd yy");
                _state.onValueChanged.AddListener((input) => _task.State = (StateTask)input);
                _assignee.onValueChanged.AddListener((input) => _task.Assignee = input);
                _task.UpdateName.AddListener(_name.SetInput);
                _task.UpdateState.AddListener((input) => _state.SetValueWithoutNotify(input));
                _task.UpdateAssignee.AddListener((input) => _assignee.SetValueWithoutNotify(input));
                _task.UpdateDescription.AddListener(_discription.SetInput);
                _removeMe.onClick.RemoveAllListeners();
                _removeMe.onClick.AddListener(Remove);
            }
        }
        [SerializeField] private InputPrefab _name;
        [SerializeField] private InputPrefab _discription;
        [SerializeField] private TMP_Text _date;
        [SerializeField] private TMP_Dropdown _state;
        [SerializeField] private TMP_Dropdown _assignee;
        [SerializeField] private Button _removeMe;
        public UnityEvent<EditTask> ImRemove
        {
            get;
            set;
        } = new UnityEvent<EditTask>();


        private void Remove()
        {
            ImRemove?.Invoke(this);
        }

        public void SetStateVariant(List<string> variants)
        {
            _state.ClearOptions();
            _state.AddOptions(variants);
            _state.SetValueWithoutNotify((int)_task.State);
        }

        public void SetAssigneeVariant(List<string> variants)
        {
            _assignee.ClearOptions();
            _assignee.AddOptions(variants);
            _assignee.SetValueWithoutNotify(_task.Assignee);
        }
    }
}