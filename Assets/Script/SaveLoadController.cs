using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    private List<ISaveLoader> _saveLoaders;
    void Start()
    {
        _saveLoaders = new List<ISaveLoader>();
        foreach(GameObject obj in gameObject.scene.GetRootGameObjects())
        {            
            foreach(Transform transform in obj.GetComponentsInChildren(typeof(Transform), true))
            {
                SearchSaveLoader(transform.gameObject);
            }
        }
        Debug.Log(_saveLoaders.Count);
        foreach(ISaveLoader load in _saveLoaders)
        {
            load.Load();
        }
        StartCoroutine(AutoSave());
    }

    private void SearchSaveLoader(GameObject obj)
    {
        ISaveLoader temp;
        obj.TryGetComponent(out temp);
        if (temp != null)
        {
            _saveLoaders.Add(temp);
        }
    }
    private IEnumerator AutoSave()
    {
        while (true)
        {            
            yield return new WaitForSeconds(10f);
            Save();
        }
    }
    private void Save()
    {
        foreach(ISaveLoader save in _saveLoaders)
        {
            save.Save();
        }
    }
}
