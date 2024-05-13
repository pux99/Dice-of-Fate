using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static CardEnemy;

public class Enemy : Fighter
{
    public UnityEvent<Enemy> CreateDiceForEnemy=new UnityEvent<Enemy>();
    public List<Rewards> rewards;
    public CardEnemy card;
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
        List<Effect.Effects> OnCombatStartStartEffects,
        List<Effect.Effects> OnTurnStartEffects,
        List<Effect.Effects> OnTakingDamageEffects,
        List<Rewards> reward,
        CardEnemy card)
    {
        _maxHealth = maxHealth;
        _health = health;
        _shield = shield;
        _OnCombatStartStartEffects = OnCombatStartStartEffects;
        _OnTakingDamageEffects = OnTakingDamageEffects;
        _OnTurnStartEffects = OnTurnStartEffects;
        rewards = reward;
        this.card = card;
        dice.Clear();
        for (int i = 0; i < diceHolder.transform.childCount; i++)
        {
            Destroy( diceHolder.transform.GetChild(i).gameObject);// cambiar para que no destrulla sino para que lo modifique
        }
        for (int i = 0; i < numbreOfDice; i++)
        {
            CreateDiceForEnemy.Invoke(this);
        }
        //for (int i = 0; i < transform.Find("Dice").childCount; i++)
        //{
        //    dice.Add(transform.Find("Dice").GetChild(i).GetComponent<Die>());
        //}
    }
}
