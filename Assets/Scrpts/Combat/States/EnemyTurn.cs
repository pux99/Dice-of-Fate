using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurn : CombatState
{
    private int stopCounter;
    public UnityEvent<int> EndOfEnemyTurn = new UnityEvent<int>();
    public UnityEvent<List<Die>> EndOfEnemyTurnDiceEfects = new UnityEvent<List<Die>>();
    bool enemyTurn;
    int attack;
    void Start()
    {
        enemyTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTurn)
        {
            StartCoroutine(StuckPrevention());
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
        List<Die> specialDice = new List<Die>();
        foreach (Die die in dieList)
        { 
            if(!die.currentFace.special)
                EndOfEnemyTurn.Invoke(die.value * attack);
            else
                specialDice.Add(die);
        }
        EndOfEnemyTurnDiceEfects.Invoke(specialDice);
        clearList();
    }
    public override void startState(List<Die> list,int damage)
    {
        base.startState(list);
        InstantMoveToContainer();
        foreach (Die die in dieList)
        {
            die.GetComponent<Collider>().enabled = true;
            die.Disolv(false);
            die.Roll();
        }
        enemyTurn = true;
        attack = damage;
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
        stepTwo();
        enemyTurn = false;

    }
}
