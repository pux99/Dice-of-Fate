using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEnemy : Card
{
    
    
    public Enemy enemy;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int shield;// minimun amount of point to get damage
    [SerializeField] private int numbreOfDice;

    [SerializeField] private List<Effect.Effects> OnCombatStartStartEffects;
    [SerializeField] private List<Effect.Effects> OnTurnStartEffects;
    [SerializeField] private List<Effect.Effects> OnTakingDamageEffects;
    [SerializeField] private bool Random;
    public List<Rewards> rewards;
    public bool button;

    public void SetUpEnemy(Enemy enemy)
    {
        enemy.SetUp(maxHealth, health, shield, numbreOfDice, OnCombatStartStartEffects, OnTurnStartEffects, OnTakingDamageEffects,rewards,this);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button)
        {
            
            button = false;
        }
    }
}
