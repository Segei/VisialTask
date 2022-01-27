using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssigneeController : MonoBehaviour, ISaveLoader
{
    private List<Assignee> _assignees;
    private SaveLoader _saveLoader = new SaveLoader();
    [SerializeField] private string _nameSave = "Assignee";

    public void Load()
    {
        List<Assignee> temp = JsonUtility.FromJson<List<Assignee>>(_saveLoader.Load(_nameSave));
        if (temp == null)
        {
            _assignees = new List<Assignee>();
        }
        else
        {
            _assignees = temp;
        }
    }

    public void Save()
    {
        Debug.Log(JsonUtility.ToJson(_assignees));
        _saveLoader.Save(_nameSave, JsonUtility.ToJson(_assignees));
    }
}
