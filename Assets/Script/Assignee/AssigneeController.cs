using Newtonsoft.Json;
using Script.Save;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Script.Assignee
{
    public class AssigneeController : MonoBehaviour, ISaveLoader, IAdded

    {
        private List<Assignee> _assignees;
        public List<Assignee> Assignees => _assignees;
        private SaveLoader _saveLoader = new SaveLoader();
        [SerializeField] private string _nameSave = "Assignee";
        [SerializeField] private List<GameObject> _views;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Button _addAssignee;
        [SerializeField] private GameObject _prefabViewAssignee;
        [SerializeField] private float _height = 50;
        [HideInInspector] public UnityEvent<List<string>> UpdateAssignee;


        public void Add()
        {
            _assignees.Add(new Assignee());
            AssigneeAddListener(_assignees.Last());
            if (_prefabViewAssignee != null)
            {
                _views.Add(CreateView(_assignees.Last()));
                StreamLineViewAssignee();
            }
        }

        public void Load()
        {
            Debug.Log("Load");
            List<Assignee> temp = JsonConvert.DeserializeObject<List<Assignee>>(_saveLoader.Load(_nameSave));
            if (_addAssignee != null)
                _addAssignee.onClick.AddListener(Add);
            if (temp == null)
            {
                _assignees = new List<Assignee>();
                _views = new List<GameObject>();
            }
            else
            {
                _assignees = temp;
                _views = new List<GameObject>();
                foreach (Assignee assignee in _assignees)
                {
                    AssigneeAddListener(assignee);
                    if (_prefabViewAssignee != null)
                        _views.Add(CreateView(assignee));

                }
            }
            if (_prefabViewAssignee != null)
                StreamLineViewAssignee();
        }

        public void Save()
        {
            Debug.Log(JsonConvert.SerializeObject(_assignees));
            _saveLoader.Save(_nameSave, JsonConvert.SerializeObject(_assignees));
        }

        private void AssigneeAddListener(Assignee assignee)
        {
            assignee.UpdateName.AddListener(UpdateAssigneeByAssineeValue);
            assignee.UpdateLastName.AddListener(UpdateAssigneeByAssineeValue);
        }

        private void UpdateAssigneeByAssineeValue(string val)
        {
            UpdateAssignee?.Invoke(GetListStringAssignee());
        }

        /// <summary>
        /// Создание отображения для сотрудника
        /// </summary>
        /// <param name="assignee"></param>
        /// <returns></returns>
        private GameObject CreateView(Assignee assignee)
        {
            RectTransform NewView = Instantiate(_prefabViewAssignee).GetComponent<RectTransform>();
            NewView.SetParent(_content);
            EditAssignee view = NewView.GetComponent<EditAssignee>();
            view.Assignee = assignee;
            view.ImRemove.AddListener(RemoveAssignee);
            UpdateAssignee?.Invoke(GetListStringAssignee());
            return NewView.gameObject;
        }

        /// <summary>
        /// Удаление сотрудника из всех листов
        /// </summary>
        /// <param name="viewAssignee"></param>
        private void RemoveAssignee(EditAssignee viewAssignee)
        {
            _views.Remove(viewAssignee.gameObject);
            Destroy(viewAssignee.gameObject);
            _assignees.Remove(viewAssignee.Assignee);
            StreamLineViewAssignee();
            UpdateAssignee?.Invoke(GetListStringAssignee());
        }

        /// <summary>
        /// Преобразование листа сотрудников в лист строк
        /// </summary>
        /// <returns></returns>
        public List<string> GetListStringAssignee()
        {
            List<string> result = new List<string>();
            foreach (Assignee assignee in _assignees)
            {
                result.Add(assignee.ToString());
            }
            return result;

        }

        /// <summary>
        /// Выправнивание элементов на интерфейсе
        /// </summary>
        private void StreamLineViewAssignee()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                RectTransform rect = _views[i].GetComponent<RectTransform>();
                rect.anchoredPosition3D = new Vector3(0, (-1 * _height / 2) + (-1 * i * _height), 0);
                rect.sizeDelta = new Vector2(0f, _height);
                rect.localScale = Vector3.one;
            }
            _content.sizeDelta = new Vector2(0, _views.Count * _height);
        }
    }
}