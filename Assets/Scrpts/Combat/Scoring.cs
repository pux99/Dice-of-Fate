using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scoring : MonoBehaviour
{
    List<List<Die>> diceList= new List<List<Die>>();
    public List<Die> SpecialDice;
    public int score;
    public GameObject container;
    bool onPlace;
    public bool timeToMove;
    public bool scoredInThisTurn;
    public UnityEvent<int> TotalPointsChange=new UnityEvent<int>();

    void Update()
    {
        if(timeToMove)
            MoveToContainer(10);
    }
    public void scoring(List<Die> List)
    {
        List<Die> list = new List<Die>();
        foreach (Die d in List)
        {
            list.Add(d);
            if (d.currentFace.special)
            {
                SpecialDice.Add(d);
            }
        }
        timeToMove = true;
        diceList.Add(list);
        calculatePoint();
        scoredInThisTurn=true;
        TotalPointsChange.Invoke(score);
    }
    public void ClearList()
    {
        //foreach (List<Die> d in diceList) 
        //{ 
        //    foreach(Die d2 in d)
        //        {
        //            d.Remove(d2);
        //        }
        //    diceList.Remove(d);//preguntar si las sitas vacias quedan en memoria?
        //}
        
        calculatePoint();
        TotalPointsChange.Invoke(score);
        SpecialDice.Clear();
        diceList.Clear();
    }
    public void calculatePoint()
    {
        int value = 0;

        foreach (List<Die> dice in diceList)
        {
            List<Die> numberDice = new List<Die>();
            List<Die> specialDice = new List<Die>();
            int localValue=0;
            foreach (var die in dice)
            {
                if (!die.currentFace.special)
                    numberDice.Add(die);
                else specialDice.Add(die);
            }
            switch (numberDice.Count)
            {
                case 1:
                    switch (numberDice[0].value)
                    {
                        case 1:
                            localValue = 100;
                            break;
                        case 5:
                            localValue = 50;
                            break;
                        default: localValue = 0; break;
                    }
                    break;
                case 2:
                    if (CheckAllEquals(numberDice)) {
                        switch (numberDice[0].value)
                        {
                            case 1:
                                localValue = 200;
                                break;
                            case 5:
                                localValue = 100;
                                break;
                            default: localValue = 0; break;
                        }
                    }  
                    break;
                case int a when a > 2:
                    if (CheckAllEquals(numberDice))
                    {
                        if (numberDice[0].value == 1)
                            localValue = numberDice[0].value * 1000 * (int)Mathf.Pow(2, a - 3);
                        else
                            localValue = numberDice[0].value * 100 * (int)Mathf.Pow(2, a - 3);
                    }
                    else localValue = 0;
                    break;
                default:
                    localValue = 0;
                    break;
            }
            foreach (Die die in specialDice)
            {
                if (die.currentFace.effect.effectData.type == EffectData.Type.multiplyDamage)
                {
                    localValue *= die.currentFace.effect.effectData.Value;
                }
            }
            value += localValue;
        }
        score = value;
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
    void MoveToContainer(float speed)
    {
        int spacing=0;
        for(int i=0;i<diceList.Count;i++)
        {
            if (i >= 1)
            {
                spacing++;
            }
            if (diceList.Count > 0)
            {
                float DieSize = diceList[i][0].size * 2;
                //int inCorectPlase = 0;
                Vector3 offset = new Vector3(-container.transform.localScale.x, 0, container.transform.localScale.z);
                
                for (int j=0;j< diceList[i]?.Count;j++)
                {
                    diceList[i][j].transform.position = Vector3.MoveTowards(diceList[i][j].transform.position,
                                                                        container.transform.position + offset * DieSize / 2 + new Vector3(+(j % 3 * (DieSize + 0.3f)), 0,-(int)(j / 3) * (DieSize + 0.3f)-2*spacing),
                                                                        speed * Time.deltaTime);//move to new position
                }
            }
            if (diceList[i].Count > 3)
            {
                spacing++;
            }
        }

        
    }
    public void updateScore()
    {
        TotalPointsChange.Invoke(score);
    }
}
