using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectInstancer : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public GameObject Die;
    public bool button;
    private void Start()
    {
        enemy.CreateDiceForEnemy.AddListener(AddDie);
    }
    private void Update()
    {
        if (button)
        {
            AddDie(player);
            button = false;
        }
    }
    public void AddDie( Fighter fighter)
    {
        GameObject newDie;
        newDie =Instantiate(Die,fighter.transform.Find("Dice"));
        fighter.dice.Add(newDie.GetComponent<Die>());
        newDie.gameObject.SetActive(false);
    }
    public void removeDie(Fighter fighter)
    {
        if (fighter.dice.Count>1)
        {
            GameObject Removed = fighter.dice[0].gameObject;
            fighter.dice.RemoveAt(0);
            Destroy(Removed);
        }
            
    }
}
