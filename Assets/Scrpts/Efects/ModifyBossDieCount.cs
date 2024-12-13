using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyBossDieCount : Effect
{
    public BoardManager board;
    public override void ApplyEffect(Fighter figther, int Value)
    {
        base.ApplyEffect(figther, Value);
        board.bossMods.diceMode += Value;
    }
}