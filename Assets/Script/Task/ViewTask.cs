using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script.Task
{
    class ViewTask : MonoBehaviour, IViewEditTask
    {
        private Task _task;
        public Task Task
        {
            get { return _task; }
            set
            {
                _task = value;
                _name.text = _task.Name;
                _discription.text = _task.Description;
                _createDate.text = _task.CreateDate.ToString("MMM dd yy");
                _state.onValueChanged.AddListener((input) => _task.State = (StateTask)input);
            }
        }
        public UnityEvent<EditTask> ImRemove { get; set; } = new UnityEvent<EditTask>();
        private List<string> _assigneeVariant = new List<string>();
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _discription;
        [SerializeField] private TMP_Text _createDate;
        [SerializeField] private Image _stateColor;
        [SerializeField] private TMP_Dropdown _state;
        [SerializeField] private TMP_Text _assignee;

        public void SetAssigneeVariant(List<string> variants)
        {
            _assigneeVariant = variants;
            _assignee.text = _assigneeVariant[_task.Assignee];
        }

        public void SetStateVariant(List<string> variants)
        {
            _state.ClearOptions();
            _state.AddOptions(variants);
            _state.onValueChanged.AddListener(SetColor);
            SetColor((int)_task.State);
            _state.SetValueWithoutNotify((int)_task.State);
        }
        private void SetColor(int color)
        {
            _stateColor.color = StateColor.Colors[(StateTask)color];
        }
    }
}
