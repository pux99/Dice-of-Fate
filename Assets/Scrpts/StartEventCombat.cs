using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEventCombat : Effect
{
    public BoardManager board;
    public UiManager uiManager;
    public void ApplyEffect(Fighter figther, int value,EnemyCard enemyCard)
    {
        base.ApplyEffect(figther, value);
        uiManager.startBattle.enemyCard = enemyCard;
        uiManager.startBattle.gameObject.SetActive(true);
    }

}
