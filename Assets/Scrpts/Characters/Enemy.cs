using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static EnemeyCard;

public class Enemy : Fighter
{
    public UnityEvent<Enemy> CreateDiceForEnemy=new UnityEvent<Enemy>();
    public List<Rewards> rewards;
    public EnemeyCard card;
    public int attack;
    public List<Die> specialdice;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (transform.GetChild(i).GetComponent<Die>() != null)
        //        dice.Add(transform.GetChild(i).GetComponent<Die>());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(
        int maxHealth,
        int health,
        int shield,
        int numbreOfDice,
        int attack,
        List<Die> specialdice,
        List<Rewards.Effect> OnCombatStartStartEffects,
        List<Rewards.Effect> OnTurnStartEffects,
        List<Rewards.Effect> OnTakingDamageEffects,
        List<Rewards> reward,
        EnemeyCard card)
    {
        _maxHealth = maxHealth;
        _health = health;
        _shield = shield;
        _OnCombatStartStartEffects = OnCombatStartStartEffects;
        _OnTakingDamageEffects = OnTakingDamageEffects;
        _OnTurnStartEffects = OnTurnStartEffects;
        rewards = reward;
        this.card = card;
        this.attack = attack;
        this.specialdice = specialdice;
        dice.Clear();
        for (int i = 0; i < diceHolder.transform.childCount; i++)
        {
            Destroy( diceHolder.transform.GetChild(i).gameObject);// cambiar para que no destrulla sino para que lo modifique
        }
        for (int i = 0; i < numbreOfDice; i++)
        {
            CreateDiceForEnemy.Invoke(this);
        }
        foreach (Die die in specialdice)
        {
            GameObject newDie =Instantiate(die.gameObject);
            newDie.transform.parent= this.diceHolder.transform;
            dice.Add(newDie.GetComponent<Die>());
            newDie.gameObject.SetActive(false);
        }
        //for (int i = 0; i < transform.Find("Dice").childCount; i++)
        //{
        //    dice.Add(transform.Find("Dice").GetChild(i).GetComponent<Die>());
        //}
    }
}
