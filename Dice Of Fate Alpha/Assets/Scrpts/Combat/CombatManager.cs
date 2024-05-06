using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class CombatManager : MonoBehaviour
{
    public RollingState rolling;
    public FlipingState flip;
    public SelectingState select;
    public Scoring scoring;
    public EnemyTurn enemyTurn;
    public Player player;
    private List<Die> OnUseDie;
    private List<Die> onScoringDie;
    public Enemy enemy;
    public int Flips=0;

    public UnityEvent combatStart=new UnityEvent();
    public UnityEvent<int> FlipValueChange=new UnityEvent<int>();
    
    
    void Start()
    {
        CombatStart();
    }
    public void EventSubscription() 
    {
        rolling.endOfRoling.AddListener(fliping);// ver que eventos se pueden remover gracias a los botones
        flip.endOfFlip.AddListener(selection);
        flip.FlipCountChange.AddListener(setFlip);
        select.ScoringEvent.AddListener(ScorePoint);
        select.RollAgain.AddListener(Rolling);
        select.RollEverythingagain.AddListener(ResetAndRoll);
        enemyTurn.EndOfEnemyTurn.AddListener(EndOfEnemyTurn);
        select.ScoringEvent.AddListener(removeDiceFromUseScoring);//dudoso
        select.ScoringEvent.AddListener(checkFlipValuePostScore);
    }

    void CombatStart()
    {
        EventSubscription();
        OnUseDie = new List<Die>();
        foreach (var die in player.dice)
        {
            OnUseDie.Add(die);
        }
    }

    void Rolling(List<Die> list)
    {
        Debug.Log(list.Count);
        Debug.Log(OnUseDie.Count);
        removeDiceFromUse(list);
        Debug.Log(OnUseDie.Count);
        rolling.startState(OnUseDie);
    }
    void fliping()
    {
        flip.startState(OnUseDie, Flips);
    }
    void selection()
    {
        select.startState(OnUseDie);
    }
    void ScorePoint(List<Die> list)
    {
        scoring.scoring(list);
        checkFlipValuePostScore(list);
    }
    public void ResetAndRoll()
    {
        OnUseDie.Clear();
        foreach (var die in player.dice)
        {
            OnUseDie.Add(die);
        }
        scoring.ClearList();
        Rolling(OnUseDie);
    }
    public void RollTheRest()
    {
        select.RollingAgain();
    }
    public void ScoringPoint()
    {
        select.ScorePoints();
    }
    void removeDiceFromUse(List<Die> list)
    {
        for (int i = 0; i < OnUseDie.Count; i++)
        {
            bool onUse=false;
            foreach(Die d2 in list)
            {
                if (OnUseDie[i] == d2)
                {
                    onUse = true;
                }
            }
            if (!onUse)
            {
                OnUseDie.RemoveAt(i);
                    i--;
            }
        }
    }
    void removeDiceFromUseScoring(List<Die> list)
    {
        for (int i = 0; i < OnUseDie.Count; i++)
        {
            bool onUse = true;
            foreach (Die d2 in list)
            {
                if (OnUseDie[i] == d2)
                {
                    onUse = false;
                }
            }
            if (!onUse)
            {
                OnUseDie.RemoveAt(i);
                i--;
            }
        }
    }
    public int flips()
    {
        return flip.FlipCount;
    }
    public void EndOfPlayerTurn()
    {
        select.ResetValues();
        enemy.Damage(scoring.score);
        Debug.Log(enemy.dice.Count);
        enemyTurn.startState(enemy.dice);
    }
    public void EndOfEnemyTurn(int value)
    {
        DamageFigther(player,value);

    }
    void DamageFigther(Fighter fighter,int value)
    {
        fighter.Damage(value);
    }
    void setFlip(int value)
    {
        Flips = value;
        FlipValueChange.Invoke(Flips);
    }
    public void modifyFlipCount(int value)
    {
        Flips += value;
        if (Flips < 0) {
            Flips = 0;
        }
        if (Flips > OnUseDie.Count) {
            Flips = OnUseDie.Count;
            
        }
        FlipValueChange.Invoke(Flips);
    }
    void checkFlipValuePostScore(List<Die> list)
    {
        modifyFlipCount(0);
    }
}
