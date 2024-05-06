using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RollingState : CombatState
{
    // Start is called before the first frame update
    private int rollingCount;
    public UnityEvent endOfRoling;
    private void Awake()
    {
        endOfRoling = new UnityEvent();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            foreach (var die in dieList)
            {
                if(die.stopRolling)
                {
                    rollingCount++;
                }
            }
            if (rollingCount == dieList.Count) 
            {
                StopCoroutine(StuckPrevention());
                endOfRoling.Invoke();
                active = false;
                clearList();
            }
            else { rollingCount = 0; }
        }
    }

    public override void startState(List<Die>list)
    {
        base.startState(list);
        foreach (var die in dieList)
        {
            die.Roll();
        }
        rollingCount=0;
        StartCoroutine(StuckPrevention());
        
    }
    IEnumerator StuckPrevention()
    {
        yield return new WaitForSeconds(5);
        foreach (Die die in dieList)
        {
            if (!die.stopRolling)
            {
                die.CheckValue();
            }
        }
        endOfRoling.Invoke();
        active = false;
        clearList();

    }
}
