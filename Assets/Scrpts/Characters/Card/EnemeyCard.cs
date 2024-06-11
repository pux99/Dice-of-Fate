using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyCard", menuName = "Card/Enemy Card")]
public class EnemeyCard : Card
{
    public Enemy enemy;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int shield;
    [SerializeField] private int numbreOfNormalDice;
    [SerializeField] private int attack;

    [SerializeField] private List<Die> specialDice;
    [SerializeField] private List<Rewards.Effect> OnCombatStartStartEffects;
    [SerializeField] private List<Rewards.Effect> OnTurnStartEffects;
    [SerializeField] private List<Rewards.Effect> OnTakingDamageEffects;
    [SerializeField] private bool Random;
    public List<Rewards> rewards;

    public void SetUpEnemy(Enemy enemy)
    {
        enemy.SetUp(maxHealth, health, shield, numbreOfNormalDice, attack, specialDice, OnCombatStartStartEffects, OnTurnStartEffects, OnTakingDamageEffects, rewards, this);

    }
}
