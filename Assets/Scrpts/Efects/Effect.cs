using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect:MonoBehaviour
{
    public enum EffectType
    {
        none,
        Heal,
        Damage,
        MaxLife,
        DiceMode
    }
    [Serializable]
    public struct Effects
    {
        public EffectType effect;
        public int value;
    }
    [SerializeField]
    protected int _value;
    public virtual void ApplyEffect(Fighter figther,int value)
    {

    }
}
