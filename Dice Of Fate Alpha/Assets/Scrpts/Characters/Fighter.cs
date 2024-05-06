using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected int _shield;// minimun amount of point to get damage
    public List<Die> dice;
    protected List<Effect> OnCombatStartStartEffects;
    protected List<Effect> OnTurnStartEffects;
    protected List<Effect> OnTakingDamageEffects;
    protected string Reward;

    public int shield { get { return _shield; } }
    public int health { get { return _health; } }
    public int maxHealth { get { return _maxHealth; } }

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
            if(OnTakingDamageEffects!=null)
            foreach (Effect effect in OnTakingDamageEffects)
                effect.ApplyEffect(this);
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
    private void defeat() 
    { 
    
    }
}
