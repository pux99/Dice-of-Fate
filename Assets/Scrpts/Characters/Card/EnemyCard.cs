using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyCard", menuName = "Card/Enemy Card")]
public class EnemyCard : Card
{
    public Enemy enemy;
    [SerializeField] private int maxHealth;
    [SerializeField] public int health;
    [SerializeField] public int shield;
    [SerializeField] public int numbreOfNormalDice;
    [SerializeField] private int attack;
    [SerializeField] public bool boss;

    [SerializeField] private List<ScriptableDie> specialDice;
    [SerializeField] private List<EffectData> OnCombatStartEffects;
    [SerializeField] private List<EffectData> OnTurnStartEffects;
    [SerializeField] private List<EffectData> OnTakingDamageEffects;
    [SerializeField] private bool Random;
    public List<EffectData> rewards;

    public void SetUpEnemy(Enemy enemy,BossModifiers mod)
    {
        if(!boss)
        {
            enemy.SetUp(maxHealth, health, shield, numbreOfNormalDice, attack, specialDice, OnCombatStartEffects, OnTurnStartEffects, OnTakingDamageEffects, rewards, this);
        }
        else
            enemy.SetUp(maxHealth, health+mod.health, shield+mod.Shield, numbreOfNormalDice+mod.diceMode, attack, specialDice, OnCombatStartEffects, OnTurnStartEffects, OnTakingDamageEffects, rewards, this);
    }
}
