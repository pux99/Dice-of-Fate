using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add1ExtraLife : Effect
{
    public override void ApplyEffect(Fighter figther, int Value)
    {
        base.ApplyEffect(figther, Value);
        figther.lives++;
    }
}
