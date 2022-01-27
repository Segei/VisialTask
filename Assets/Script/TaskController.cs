using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour, ISaveLoader
{
    private List<Task> _tasks = new List<Task>();
    private SaveLoader _saveLoader = new SaveLoader();
    [SerializeField] private string _nameSave = "Task";

    public void Load()
    {
        List<Task> temp = JsonUtility.FromJson<List<Task>>(_saveLoader.Load(_nameSave));
        if (temp == null)
        {
            _tasks = new List<Task>();
        }
        else
        {
            _tasks = temp;
        }
    }

    public void Save()
    {
        Debug.Log(JsonUtility.ToJson(_tasks));
        _saveLoader.Save(_nameSave, JsonUtility.ToJson(_tasks));
    }
}
