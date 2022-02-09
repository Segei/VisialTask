using System.Collections.Generic;
using UnityEngine.Events;


namespace Script.Task
{
    interface IViewEditTask
    {
        public Task Task
        {
            get;
            set;
        }
        public UnityEvent<EditTask> ImRemove
        {
            get;
            set;
        }


        public void SetStateVariant(List<string> variants);

        public void SetAssigneeVariant(List<string> variants);
    }
}
