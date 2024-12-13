using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBattleStarter : MonoBehaviour
{
    public EnemyCard enemyCard;
    public BoardManager boardManager;
    // Start is called before the first frame update
    public void startBattle()
    {
        boardManager.startCombat(enemyCard);
    }
}
