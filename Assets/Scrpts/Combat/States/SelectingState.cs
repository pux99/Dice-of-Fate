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
    private bool _pointsScored;
    public bool pointsScored
    {
        get { return _pointsScored; }
    }

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
        _pointsScored = false;
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
    public void calculatePoint()
    {
        int value = 0;
        List<Die> numberDice = new List<Die>();
        List<Die> specialDice = new List<Die>();
        foreach (var die in selected)
        {
            if(!die.currentFace.special)
                numberDice.Add(die);
            else specialDice.Add(die);
        }
        switch (numberDice.Count)
        {
            case 1:
                switch (numberDice[0].value)
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
                if (CheckAllEquals(numberDice))
                {
                    switch (numberDice[0].value)
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
                if (CheckAllEquals(numberDice))
                {
                    if (numberDice[0].value == 1)
                        value = numberDice[0].value * 1000 * (int)Mathf.Pow(2, a - 3);
                    else
                        value = numberDice[0].value * 100 * (int)Mathf.Pow(2, a - 3);
                }
                else value = 0;
                break;
            default:
                value = 0;
                break;
        }
        foreach (Die die in specialDice)
        {
            if (die.currentFace.effect.effectData.type == EffectData.Type.multiplyDamage)
            {
                value *= die.currentFace.effect.effectData.Value;
            }
        }
         _points=value;
    }
    public bool CheckAllEquals(List<Die> list)
    {
        int value = list[0].value;
        bool allEquals = true;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].value != value )
            {
                allEquals = false; break;
            }
        }
        return allEquals;
    }
    public void RollingAgain()
    {
        if (_pointsScored)
        {
            foreach (Die die in dieList)
            {
                die.selectable = false;
                die._selected = false;
                die.turnOffOutline();
                die.select.RemoveListener(ChangeSelected);
            }
            RollAgain.Invoke(dieList);
            dieList.Clear();
            selected.Clear();
            calculatePoint();
        }
        
    }
    public void ScorePoints()
    {
        if (selected.Count > 0)
        {
            bool allSpecial = true;
            foreach (Die die in selected)
            {
                if (!die.currentFace.special)
                {
                    allSpecial = false;
                }
            }
            if (_points > 0 || allSpecial)
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
                _points = 0;
                _pointsScored = true;
            }
        }
       
    }
    public void ResetValues()
    {
        foreach (Die die in dieList)
        {
            die.selectable = false;
            die.select.RemoveListener(ChangeSelected);
        }
        selected.Clear();
        dieList.Clear();
    }
}
