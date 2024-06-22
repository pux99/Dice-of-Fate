using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static EnemyCard;

public class Enemy : Fighter
{
    public UnityEvent<Enemy> CreateDiceForEnemy=new UnityEvent<Enemy>();
    public List<Rewards> rewards;
    public EnemyCard card;
    public int attack;
    public List<Die> specialdice;
    public GameObject BaseDie;
    
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
        List<Rewards> reward,
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
        //this.specialdice = specialdice;
        dice.Clear();
        for (int i = 0; i < diceHolder.transform.childCount; i++)
        {
            Destroy( diceHolder.transform.GetChild(i).gameObject);// cambiar para que no destrulla sino para que lo modifique
        }
        for (int i = 0; i < numbreOfDice; i++)
        {
            CreateDiceForEnemy.Invoke(this);
        }
        for (int i = 0; i < specialdice.Count; i++)
        {
            GameObject newDie = Instantiate(BaseDie);
            newDie.transform.parent = this.diceHolder.transform;
            Die script = newDie.GetComponent<Die>();
            script.ChangeDiePropertys(specialdice[i]);
            dice.Add(script);
            newDie.gameObject.SetActive(false);
        }
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
