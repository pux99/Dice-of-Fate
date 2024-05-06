using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAmountMod : Effect
{
    public GameObjectInstancer instancer;
    public override void ApplyEffect(Fighter figther, int Value)
    {
        base.ApplyEffect(figther, Value);
        if (Value > 0)
        {
            for (int i = 0; i < Value; i++)
            {
                instancer.AddDie(figther);
            }
        }
        else
        {
            for (int i = 0; i < Value * -1; i++)
            {
                instancer.removeDie(figther);
            }
        }
    }
}
