using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectInstancer : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public GameObject Die;
    private void Start()
    {
        enemy.CreateDiceForEnemy.AddListener(AddDie);
    }
    public void AddDie( Fighter fighter)
    {
        GameObject newDie;
        newDie =Instantiate(Die);
        Die newDieScript = newDie.GetComponent<Die>();
        fighter.AddDie(newDieScript);
        newDieScript.disolvTarget = 1;
        newDieScript.freez();
    }
    public void removeDie(Fighter fighter)
    {
        if (fighter.dice.Count>1)
        {
            GameObject Removed = fighter.dice[0].gameObject;
            fighter.RemoveDie(Removed.GetComponent<Die>());
        }
            
    }
}
