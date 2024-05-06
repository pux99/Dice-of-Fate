using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scoring : MonoBehaviour
{
    List<List<Die>> diceList= new List<List<Die>>();
    public int score;
    public GameObject container;
    bool onPlace;
    public UnityEvent<int> TotalPointsChange=new UnityEvent<int>();

    void Update()
    {
        MoveToContainer(10);
    }
    public void scoring(List<Die> List)
    {
        List<Die> list = new List<Die>();
        foreach (Die d in List)
        {
            list.Add(d);
        }
        diceList.Add(list);
        calculatePoint();
        TotalPointsChange.Invoke(score);
        Debug.Log("list"+List.Count);
        Debug.Log(diceList.Count);
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
        diceList.Clear();
    }
    void calculatePoint()
    {
        int value = 0;
        foreach (List<Die> dice in diceList)
        {
            switch (dice.Count)
            {
                case 1:
                    switch (dice[0].value)
                    {
                        case 1:
                            value += 100;
                            break;
                        case 5:
                            value += 50;
                            break;
                        default: value += 0; break;
                    }
                    break;
                case 2:
                    if (CheckAllEquals(dice)) {
                        switch (dice[0].value)
                        {
                            case 1:
                                value += 200;
                                break;
                            case 5:
                                value += 100;
                                break;
                            default: value += 0; break;
                        }
                    }  
                    break;
                case int a when a > 2:
                    if (CheckAllEquals(dice))
                    {
                        if (dice[0].value == 1)
                            value += dice[0].value * 1000 * (int)Mathf.Pow(2, a - 3);
                        else
                            value += dice[0].value * 100 * (int)Mathf.Pow(2, a - 3);
                    }
                    else value += 0;
                    break;
                default:
                    value += 0;
                    break;
            }
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
                
                for (int j=0;j< diceList[i].Count;j++)
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
}
