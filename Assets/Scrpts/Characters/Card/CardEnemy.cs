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
    [SerializeField] private int numbreOfNormalDice;
    [SerializeField] private int attack;

    [SerializeField] private List<Die> specialDice;
    [SerializeField] private List<Rewards.Effect> OnCombatStartStartEffects;
    [SerializeField] private List<Rewards.Effect> OnTurnStartEffects;
    [SerializeField] private List<Rewards.Effect> OnTakingDamageEffects;
    [SerializeField] private bool Random;
    public List<Rewards> rewards;
    public bool button;

    public void SetUpEnemy(Enemy enemy)
    {
        enemy.SetUp(maxHealth, health, shield, numbreOfNormalDice,attack, specialDice, OnCombatStartStartEffects, OnTurnStartEffects, OnTakingDamageEffects,rewards,this);
        
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
