using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurn : CombatState
{
    private int stopCounter;
    public UnityEvent<int> EndOfEnemyTurn = new UnityEvent<int>();
    bool enemyTurn;
    void Start()
    {
        enemyTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTurn)
        {
            foreach (Die die in dieList)
            {
                if (die.stopRolling)
                {
                    stopCounter++;
                }
            }
            if (stopCounter == dieList.Count)
            {
                stepTwo();
                enemyTurn = false;
            }
            else
            {
                stopCounter = 0;
            }
        }
        
    }
    public void stepTwo()
    {
        foreach(Die die in dieList)
            EndOfEnemyTurn.Invoke(die.value*100);
        clearList();
    }
    public override void startState(List<Die> list)
    {
        base.startState(list);
        foreach (Die die in dieList)
        {
            
            die.Roll();
        }
        enemyTurn = true;
    }
}
