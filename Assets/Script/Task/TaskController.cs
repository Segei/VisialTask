using Newtonsoft.Json;
using Script.Assignee;
using Script.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Script.Task
{
    public class TaskController : MonoBehaviour, ISaveLoader, IAdded
    {
        private List<Task> _tasks = new List<Task>();
        public List<Task> Tasks => _tasks;
        private SaveLoader _saveLoader = new SaveLoader();
        [SerializeField] private string _nameSave = "Task";
        [SerializeField] private List<GameObject> _views;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Button _addTask;
        [SerializeField] private GameObject _prefabViewTask;
        [SerializeField] private float _height = 50;
        [SerializeField] private AssigneeController _assigneeController;
        [HideInInspector] public AssigneeController AssigneeController => _assigneeController;


        public void Add()
        {
            _tasks.Add(new Task());
            if (_prefabViewTask != null)
            {
                _views.Add(CreateView(_tasks.Last()));
                StreamLineViewAssignee();
            }
        }

        public void Load()
        {
            Debug.Log("Load");
            List<Task> temp = JsonConvert.DeserializeObject<List<Task>>(_saveLoader.Load(_nameSave));
            if (_addTask != null)
                _addTask.onClick.AddListener(Add);
            if (temp == null)
            {
                _tasks = new List<Task>();
                _views = new List<GameObject>();
            }
            else
            {
                _tasks = temp;
                _views = new List<GameObject>();
                if (_prefabViewTask != null)
                {
                    foreach (Task task in _tasks)
                    {
                        _views.Add(CreateView(task));
                    }
                }
            }
            if (_prefabViewTask != null)
                StreamLineViewAssignee();
        }

        public void Save()
        {
            Debug.Log(JsonConvert.SerializeObject(_tasks));
            _saveLoader.Save(_nameSave, JsonConvert.SerializeObject(_tasks));
        }

        /// <summary>
        /// Создание отображения для задачи
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private GameObject CreateView(Task task)
        {
            RectTransform NewView = Instantiate(_prefabViewTask).GetComponent<RectTransform>();
            NewView.SetParent(_content);
            IViewEditTask view = NewView.GetComponent<IViewEditTask>();
            view.Task = task;
            view.ImRemove.AddListener(RemoveTask);
            view.SetStateVariant(Enum.GetNames(typeof(StateTask)).ToList());
            view.SetAssigneeVariant(_assigneeController.GetListStringAssignee());
            _assigneeController.UpdateAssignee.AddListener(view.SetAssigneeVariant);
            return NewView.gameObject;
        }

        /// <summary>
        /// Удаление задачи из всех листов
        /// </summary>
        /// <param name="viewTask"></param>
        private void RemoveTask(EditTask viewTask)
        {
            _views.Remove(viewTask.gameObject);
            Destroy(viewTask.gameObject);
            _tasks.Remove(viewTask.Task);
            StreamLineViewAssignee();
        }

        /// <summary>
        /// Выправнивание элементов на интерфейсе
        /// </summary>
        private void StreamLineViewAssignee()
        {
            for (int i = 0; i < _views.Count; i++)
            {
                RectTransform rect = _views[i].GetComponent<RectTransform>();
                rect.anchoredPosition3D = new Vector3(0, -1 * _height / 2 + -1 * i * _height, 0);
                rect.sizeDelta = new Vector2(0f, _height);
                rect.localScale = Vector3.one;
            }
            _content.sizeDelta = new Vector2(0, _views.Count * _height);
        }
    }
}