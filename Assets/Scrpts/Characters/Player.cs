using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{

    public UnityEvent PlayerDefeted=new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.Find("Dice").childCount; i++)
        {
            dice.Add(transform.Find("Dice").GetChild(i).GetComponent<Die>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
