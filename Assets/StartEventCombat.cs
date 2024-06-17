using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventCombat : Effect
{
    public BoardManager board;
    public void ApplyEffect(Fighter figther, int value,EnemyCard enemyCard)
    {
        base.ApplyEffect(figther, value);
        board.startCombat(enemyCard);
    }

}
