using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static EnemyCard;

public class Enemy : Fighter
{
    public UnityEvent<Enemy> CreateDiceForEnemy=new UnityEvent<Enemy>();
    public List<EffectData> rewards;
    public EnemyCard card;
    public int attack;
    

    public void SetUp(
        int maxHealth,
        int health,
        int shield,
        int numbreOfDice,
        int attack,
        List<ScriptableDie> specialdice,
        List<EffectData> OnCombatStartEffects,
        List<EffectData> OnTurnStartEffects,
        List<EffectData> OnTakingDamageEffects,
        List<EffectData> reward,
        EnemyCard card)
    {
        _maxHealth = maxHealth;
        _health = health;
        _shield = shield;
        _OnCombatStartEffects = OnCombatStartEffects;
        _OnTakingDamageEffects = OnTakingDamageEffects;
        _OnTurnStartEffects = OnTurnStartEffects;
        rewards = reward;
        this.card = card;
        this.attack = attack;
        this.normalDieCount = numbreOfDice;
        this.specialdice = specialdice;
        dice.Clear();
        for (int i = 0; i < diceHolder.transform.childCount; i++)
        {
            Destroy( diceHolder.transform.GetChild(i).gameObject);// cambiar para que no destrulla sino para que lo modifique
        }
        GenerateDie();
        //for (int i = 0; i < numbreOfDice; i++)
        //{
        //    CreateDiceForEnemy.Invoke(this);
        //}
        //for (int i = 0; i < specialdice.Count; i++)
        //{
        //    GameObject newDie = Instantiate(BaseDie);
        //    newDie.transform.parent = this.diceHolder.transform;
        //    Die script = newDie.GetComponent<Die>();
        //    script.ChangeDiePropertys(specialdice[i]);
        //    dice.Add(script);
        //    newDie.gameObject.SetActive(false);
        //}
        //foreach (Die die in specialdice)
        //{
        //    GameObject newDie =Instantiate(die.gameObject);
        //    newDie.transform.parent= this.diceHolder.transform;
        //    dice.Add(newDie.GetComponent<Die>());
        //    newDie.gameObject.SetActive(false);
        //}
        //for (int i = 0; i < transform.Find("Dice").childCount; i++)
        //{
        //    dice.Add(transform.Find("Dice").GetChild(i).GetComponent<Die>());
        //}
    }
}
