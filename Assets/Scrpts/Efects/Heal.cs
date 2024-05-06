using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Effect
{
    public override void ApplyEffect(Fighter figther, int Value)
    {
        base.ApplyEffect(figther, Value);
        figther.Heal(Value);
    }
}