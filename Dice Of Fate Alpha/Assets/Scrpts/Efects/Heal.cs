using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Effect
{
    public override void ApplyEffect(Fighter figther)
    {
        base.ApplyEffect(figther);
        figther.Heal(_value);
    }
}