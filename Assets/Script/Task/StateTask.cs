using System.Collections.Generic;
using UnityEngine;

namespace Script.Task
{
    public enum StateTask
    {
        Open,
        InProgress,
        Complited
    }
    public static class StateColor
    {
        public static Dictionary<StateTask, Color> Colors = new Dictionary<StateTask, Color>()
        {
            { StateTask.Open, Color.gray },
            { StateTask.InProgress, new Color(0.7490196f, 0.3215686f, 0, 1) },
            { StateTask.Complited, Color.black }
        };
    }

}