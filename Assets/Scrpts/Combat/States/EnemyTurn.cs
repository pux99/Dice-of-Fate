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
        StopAllCoroutines();
        List<Die> specialDice = new List<Die>();
        int damage=0;
        foreach (Die die in dieList)
        { 
            if(!die.currentFace.special)
                damage+=die.value * attack;
            else
                specialDice.Add(die);
        }
        EndOfEnemyTurnDiceEfects.Invoke(specialDice);
        EndOfEnemyTurn.Invoke(damage);
        clearList();
        stopCounter = 0;
    }
    public override void startState(List<Die> list,int damage)
    {
        base.startState(list);
        InstantMoveToContainer();
        foreach (Die die in dieList)
        {
            die.GetComponent<Collider>().enabled = true;
            die.Disolv(false);
            die.gameObject.transform.position=container.transform.position+new Vector3(Random.Range(-2f,2f),0, Random.Range(-2f, 2f));
            die.Roll();
        }
        StartCoroutine(StuckPrevention());
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
