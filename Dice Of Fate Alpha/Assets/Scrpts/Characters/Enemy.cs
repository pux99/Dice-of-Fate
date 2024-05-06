using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Die>() != null)
                dice.Add(transform.GetChild(i).GetComponent<Die>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
