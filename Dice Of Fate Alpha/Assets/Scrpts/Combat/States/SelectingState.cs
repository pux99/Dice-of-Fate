using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectingState : CombatState
{
    [SerializeField]private int _points;
    List<Die> selected=new List<Die>();
    public UnityEvent<List<Die>> ScoringEvent;
    public UnityEvent<List <Die>> RollAgain;
    public UnityEvent RollEverythingagain;
    public UnityEvent<int> SelectedPointsChange;

    private void Awake()
    {
        ScoringEvent =new UnityEvent<List<Die>>();
        RollEverythingagain=new UnityEvent();
        RollAgain=new UnityEvent<List<Die>>();
        SelectedPointsChange = new UnityEvent<int>();
    }
    void Update()
    {
        
    }
    public override void startState(List<Die> list)
    {
        base.startState(list);
        foreach (var die in dieList)
        {
            die.selectable = true;
            die.select.AddListener(ChangeSelected);
        }
    }
    void ChangeSelected(bool state,Die die)
    {
        if (state&& !selected.Contains(die))
        {
            selected.Add(die);
        }
        else 
            if (selected.Contains(die))
               selected.Remove(die);
        calculatePoint();
        SelectedPointsChange.Invoke(_points);


    }
    void calculatePoint()
    {
        int value = 0;
        switch (selected.Count)
        {
            case 1:
                switch (selected[0].value)
                {
                    case 1:
                        value = 100;
                        break;
                    case 5:
                        value = 50;
                        break;
                    default: value = 0; break;
                }
                break;
            case 2:
                if (CheckAllEquals(selected))
                {
                    switch (selected[0].value)
                    {
                        case 1:
                            value = 200;
                            break;
                        case 5:
                            value = 100;
                            break;
                        default: value = 0; break;
                    }
                }
                break;
            case int a when a > 2:
                if (CheckAllEquals(selected))
                {
                    if (selected[0].value == 1)
                        value = selected[0].value * 1000 * (int)Mathf.Pow(2, a - 3);
                    else
                        value = selected[0].value * 100 * (int)Mathf.Pow(2, a - 3);
                }
                else value = 0;
                break;
            default:
                value = 0;
                break;
        }
         _points=value;
    }
    public bool CheckAllEquals(List<Die> list)
    {
        int value = list[0].value;
        bool allEquals = true;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].value != value)
            {
                allEquals = false; break;
            }
        }
        return allEquals;
    }
    public void RollingAgain()
    {
        foreach (Die die in dieList)
        {
            die.selectable = false;
            die.select.RemoveListener(ChangeSelected);
        }
        Debug.Log(dieList.Count);
        RollAgain.Invoke(dieList);
        dieList.Clear();
    }
    public void ScorePoints()
    {
        if (_points > 0)
        {
            foreach (Die die in selected)
            {
                die.selectable = false;
                die.select.RemoveListener(ChangeSelected);
                die.turnOffOutline();
                dieList.Remove(die);
            }
            ScoringEvent.Invoke(selected);
            //removeFormList(selected);
            selected.Clear();
            calculatePoint();
            SelectedPointsChange.Invoke(_points);
        }
    }
    public void ResetValues()
    {
        foreach (Die die in dieList)
        {
            die.selectable = false;
            die.select.RemoveListener(ChangeSelected);
        }
        dieList.Clear();
    }
}
