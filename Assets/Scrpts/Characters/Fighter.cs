using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected int _shield;// minimun amount of point to get damage
    [SerializeField] public GameObject diceHolder;
    [SerializeField] public int normalDieCount;
    public List<ScriptableDie> specialdice;
    public List<Die> dice;
    public List<EffectData> _OnCombatStartEffects;
    public List<EffectData> _OnTurnStartEffects;
    public List<EffectData> _OnTakingDamageEffects;
    protected string Reward;
    public int lives=1;
    public bool SkipNextTurn;
    [SerializeField]private EffectApllier effectApllier;
    public GameObject BaseDie;

    public int shield { get { return _shield; } }
    public int health { get { return _health; } }
    public int maxHealth { get { return _maxHealth; } }
    public UnityEvent Defeted = new UnityEvent();
    public UnityEvent<Fighter> UpdateHealthBar=new UnityEvent<Fighter>();


    public void Heal(int value)
    {
        _health += value;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        UpdateHealthBar.Invoke(this); 
    }
    public void Damage(int value)
    {
        if (value > _shield)
        {
           if(_OnTakingDamageEffects!=null)
            foreach (EffectData effect in _OnTakingDamageEffects)
                effectApllier.ApplyEffect(effect);
            _health -= value;
            if (_health <= 0)
            {
                _health = 0;
                UpdateHealthBar.Invoke(this);
                lives--;
                if(lives<=0)
                    defeat();
                else
                    Revive();
            }
            else
            {
                UpdateHealthBar.Invoke(this);
            }
        } 
    }
    public void  DamageEffect(int value)
    {
            if (_OnTakingDamageEffects != null)
                foreach (EffectData effect in _OnTakingDamageEffects)
                    effectApllier.ApplyEffect(effect);
            _health -= value;
            if (_health <= 0)
            {
                _health = 0;
                UpdateHealthBar.Invoke(this);
                lives--;
                if (lives <= 0)
                    defeat();
                else
                    Revive();
            }
            else
            {
                UpdateHealthBar.Invoke(this);
            }

    }
    public void ChangeMaxHealth(int value)
    {
        _maxHealth += value;
        if (_maxHealth< 0) 
        {
            _maxHealth = 1;
        }
    }
    public void TurnOnOffDice(bool state)
    {
        foreach(Die die in dice)
        {
            die.gameObject.SetActive(state);
        }
    }
    private void defeat() 
    {
        Defeted.Invoke();
    }
    private void Revive()
    {
        // efecto estetico
        _health = _maxHealth / 2;
    }
    public void OnTurnStart()
    {
        foreach (EffectData effect in _OnTurnStartEffects)
            effectApllier.ApplyEffect(effect);
    }
    public void OnStartOfBattle()
    {
        foreach (EffectData effect in _OnCombatStartEffects)
            effectApllier.ApplyEffect(effect);
    }
    public void GenerateDie()
    {
        for (int i = 0; i < normalDieCount; i++)
        {
            GameObject newDie= Instantiate(BaseDie, diceHolder.transform);
            dice.Add(newDie.GetComponent<Die>());
        }
        foreach(ScriptableDie dieData in specialdice)
        {
            GameObject newDie = Instantiate(BaseDie, diceHolder.transform);
            Die newDieScript = newDie.GetComponent<Die>();
            newDieScript.ChangeDiePropertys(dieData);
            dice.Add(newDieScript);
        }
        foreach(Die d in dice)
        {
            d.freez();
        }
    }
    public void AddDie(Die ndie)
    {
        ndie.gameObject.transform.parent=diceHolder.transform;
        dice.Add(ndie);
        ndie.Roll();
        normalDieCount++;
    }
    public void RemoveDie(Die odie) 
    {
        dice.Remove(odie);
        Destroy(odie.gameObject);
        normalDieCount--;
    }
}
