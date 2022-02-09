using BarGraph.VittorCloud;
using Script.Assignee;
using Script.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewStatistic : MonoBehaviour
{
    [SerializeField] private TaskController _taskController;
    [SerializeField] private BarGraphExample _barGraph;
    [SerializeField] private PieChart.ViitorCloud.PieChart _pieChart;
    [SerializeField] private RectTransform _conteiner;
    [SerializeField] private GameObject _preafab;
    [SerializeField] private float _sizePanel = 50;
    private List<StateToStatistic> _stateToStatistic;
    public void Load()
    {
        _stateToStatistic = GetStatistics(_taskController);
        if (_barGraph != null)
        {
            _barGraph.exampleDataSet = GetBarGraphDatas();
            _barGraph.gameObject.GetComponent<BarGraphGenerator>().OnInitialGraphCompleted.AddListener(ViewStateLegend);
        }
        if (_pieChart != null)
        {
            FullPieChart(_pieChart);
        }
    }

    private List<BarGraphDataSet> GetBarGraphDatas()
    {
        List<BarGraphDataSet> data = new List<BarGraphDataSet>();
        foreach (StateToStatistic state in _stateToStatistic)
        {
            BarGraphDataSet dataSet = new BarGraphDataSet();
            dataSet.ListOfBars = new List<XYBarValues>();
            dataSet.GroupName = " ";
            dataSet.barColor = StateColor.Colors[(StateTask)Enum.Parse(typeof(StateTask), state.Name)];
            foreach (AssigneeToStatistic assignee in state.Assignees)
            {
                XYBarValues xYBarValues = new XYBarValues();
                xYBarValues.XValue = assignee.Name;
                xYBarValues.YValue = assignee.Count;
                dataSet.ListOfBars.Add(xYBarValues);
            }
            data.Add(dataSet);
        }
        return data;
    }

    private void ViewStateLegend()
    {
        if (_conteiner != null)
        {
            _conteiner.sizeDelta = new Vector2(_sizePanel * _stateToStatistic.Count, _sizePanel);
            _conteiner.anchoredPosition3D = new Vector3(-_conteiner.sizeDelta.x/2, -_conteiner.sizeDelta.y/2, 0);
            float lastposition = _sizePanel/2;
            foreach(var state in _stateToStatistic)
            {
                GameObject temp = Instantiate(_preafab);
                temp.transform.SetParent(_conteiner);
                temp.GetComponent<Image>().color = StateColor.Colors[(StateTask)Enum.Parse(typeof(StateTask), state.Name)];
                temp.GetComponentInChildren<TMP_Text>().text = state.Name;
                temp.GetComponent<RectTransform>().sizeDelta = new Vector2(_sizePanel, 0);
                temp.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(lastposition, 0);
                lastposition += _sizePanel;
            }
        }
    }
    private void FullPieChart(PieChart.ViitorCloud.PieChart pieChart)
    {
        Debug.Log("FullPieChart");
        pieChart.dataDescription.Clear();
        pieChart.Data = new float[_stateToStatistic.Count];
        pieChart.customColors = new Color[_stateToStatistic.Count];
        pieChart.segments = _stateToStatistic.Count;
        for (int i = 0; i < _stateToStatistic.Count; i++)
        {
            pieChart.dataDescription.Add(_stateToStatistic[i].Name);
            pieChart.Data[i] = GetAllAssigneesCount(_stateToStatistic[i]);
            pieChart.customColors[i] = StateColor.Colors[(StateTask)Enum.Parse(typeof(StateTask), _stateToStatistic[i].Name)];
        }
    }

    private int GetAllAssigneesCount(StateToStatistic state)
    {
        int result = 0;
        foreach (var item in state.Assignees)
        {
            result += item.Count;
        }
        return result;
    }
    private List<StateToStatistic> GetStatistics(TaskController taskController)
    {
        List<StateToStatistic> statistics = new List<StateToStatistic>();
        var state = Enum.GetNames(typeof(StateTask));
        List<string> assignee = new List<string>();
        foreach (Assignee item in taskController.AssigneeController.Assignees)
        {
            assignee.Add(item.ToString());
        }
        for (int i = 0; i < state.Length; i++)
        {
            StateToStatistic stateTo = new StateToStatistic();
            stateTo.Name = state[i];
            foreach (string item in assignee)
            {
                AssigneeToStatistic temp = new AssigneeToStatistic();
                temp.Name = item;
                stateTo.Assignees.Add(temp);
            }
            statistics.Add(stateTo);
        }
        foreach (Task task in taskController.Tasks)
        {
            statistics.Where(item => Enum.GetName((typeof(StateTask)), task.State) == item.Name).Single().Assignees[task.Assignee].Count++;
        }
        return statistics;
    }
}
class StateToStatistic
{
    public string Name;
    public List<AssigneeToStatistic> Assignees = new List<AssigneeToStatistic>();
}
class AssigneeToStatistic
{
    public string Name;
    public int Count;
}