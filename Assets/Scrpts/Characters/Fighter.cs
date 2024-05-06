using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected int _shield;// minimun amount of point to get damage
    [SerializeField] protected GameObject diceHolder;
    public List<Die> dice;
    protected List<Effect.Effects> _OnCombatStartStartEffects;
    protected List<Effect.Effects> _OnTurnStartEffects;
    protected List<Effect.Effects> _OnTakingDamageEffects;
    protected string Reward;

    public int shield { get { return _shield; } }
    public int health { get { return _health; } }
    public int maxHealth { get { return _maxHealth; } }
    public UnityEvent Defeted = new UnityEvent();
    public UnityEvent<Fighter> UpdateHealthBar=new UnityEvent<Fighter>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Heal(int value)
    {
        _health += value;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
    public void Damage(int value)
    {
        if (value > _shield)
        {
            if(_OnTakingDamageEffects!=null)
            foreach (Effect.Effects effect in _OnTakingDamageEffects)
                //effect.ApplyEffect(this,1);
            _health -= value;
            if (_health <= 0)
            {
                _health = 0;
                UpdateHealthBar.Invoke(this);
                defeat();
            }
            else
            {
                UpdateHealthBar.Invoke(this);
            }
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
}
