using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Script.Save
{
    public class SaveLoadController : MonoBehaviour
    {
        private List<ISaveLoader> _saveLoaders;
        [SerializeField] private GameObject _assignee;
        [SerializeField] private GameObject _task;
        [SerializeField] private ViewStatistic _viewStatistic;



        void Awake()
        {
            _saveLoaders = new List<ISaveLoader>();
            _saveLoaders.Add(_assignee.GetComponent<ISaveLoader>());
            _saveLoaders.Add(_task.GetComponent<ISaveLoader>());            
            foreach(var loader in _saveLoaders)
            {
                loader.Load();
            }
            StartCoroutine(AutoSave());
            if (_viewStatistic != null)
            {
                _viewStatistic.Load();
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

        public void ManualSave()
        {
            Save();
        }

        private void Save()
        {
            foreach(ISaveLoader saver in _saveLoaders)
            {
                saver.Save();
            }
        }

    }
}