using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UI.GridLayoutGroup;

public class CombatManager : MonoBehaviour
{
    public RollingState rolling;
    public FlipingState flip;
    public SelectingState select;
    public Scoring scoring;
    public EnemyTurn enemyTurn;
    public BattleLog Log;
    public SoundAudioClip audioManager;

    public Player player;
    public Enemy enemy;

    private List<Die> OnUseDie= new List<Die>();
    public List<Die> pOnUseDie { get { return OnUseDie; } }
    
    public int Flips=0;

    public EffectApllier effectApllier;

    public UnityEvent combatStart=new UnityEvent();
    public UnityEvent<int> FlipValueChange=new UnityEvent<int>();
    public UnityEvent<string> win = new UnityEvent<string>();
    public UnityEvent<string> TextoDeEffectos = new UnityEvent<string>();
    public UnityEvent loss = new UnityEvent();


    void Start()
    {
        EventSubscription();
        //CombatStart(player,enemy);
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
        enemyTurn.EndOfEnemyTurnDiceEfects.AddListener(EndOfEnemyTurnDiceEfects);


    }

    public void CombatStart(Player NewPlayer,Enemy NewEnemy)
    {
        player = NewPlayer;
        enemy = NewEnemy;
        player.TurnOnOffDice(true);
        enemy.TurnOnOffDice(true);
        player.Defeted.AddListener(Loss);
        enemy.Defeted.AddListener(Win);
        player.GenerateDie();
        OnUseDie.Clear();
        player.OnStartOfBattle();
        enemy.OnStartOfBattle();
        player.OnTurnStart();
        foreach (var die in player.dice)
        {
            OnUseDie.Add(die);
            die.freez();
        }
        SoundAudioClip.instance.Destroymusic();
        if(enemy.card.boss)
        {
            SoundManager.PlayMusic(SoundManager.Sound.BossMusic, true);
        }
        else
            SoundManager.PlayMusic(SoundManager.Sound.EnemyMusic,true);
        combatStart.Invoke();
        scoring.updateScore();
    }

    void Rolling(List<Die> list)
    {
        removeDiceFromUse(list);
        rolling.startState(OnUseDie);
    }
    void fliping()
    {
        flip.startState(OnUseDie, Flips);
    }
    void selection()
    {
        select.startState(OnUseDie);
        scoring.calculatePoint();
    }
    void ScorePoint(List<Die> list)
    {
        scoring.scoring(list);
        checkFlipValuePostScore(list);
    }
    public void ResetAndRoll()
    {
        foreach (var die in enemy.dice)
        {
            die.Disolv(true);
            die.GetComponent<Collider>().enabled = false;
        }
        //OnUseDie.Clear();
        //foreach (var die in player.dice)
        //{
        //    OnUseDie.Add(die);
        //}
        scoring.ClearList();
        Rolling(OnUseDie);
    }
    public void RollTheRest()
    {
        select.RollingAgain();
        scoring.scoredInThisTurn = false;
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
        if(scoring.scoredInThisTurn)
            DamageFigther(enemy,scoring.score);
        else
        {
            //un log
        }
        scoring.scoredInThisTurn = false;
        Log.AddLog("<color=#"+ ColorUtility.ToHtmlStringRGB(enemy.color) +">" + enemy.name + "</color>" + " perdio " + scoring.score + " puntos de vida");
        scoring.score = 0;
        scoring.TotalPointsChange.Invoke(scoring.score);
        ApllyDiceEffects(scoring.SpecialDice, enemy, player);
        if (enemy.health > 0)
        {
            enemy.OnTurnStart();
        }     
        foreach (Die die in player.dice)
        {
            die.Disolv(true);
            die._selected = false;
            die.turnOffOutline();
        }
        select.calculatePoint();
        scoring.timeToMove = false;
        if (enemy.health > 0 && !enemy.SkipNextTurn)
            enemyTurn.startState(enemy.dice, enemy.attack);
        else if (enemy.SkipNextTurn)
        {
            enemyTurn.EndOfEnemyTurn.Invoke(0);
            enemy.SkipNextTurn=false;
        }
        OnUseDie.Clear();
        foreach (var die in player.dice)
        {
            OnUseDie.Add(die);
            die.freez();
        }
        scoring.ClearList();
        scoring.calculatePoint();
    }
    public void EndOfEnemyTurn(int value)
    {
        player.Damage(value);
        Log.AddLog("<color=#" + ColorUtility.ToHtmlStringRGB(player.color)+ ">" + player.name + "</color>" + " perdio " + value + " puntos de vida");
        player.OnTurnStart();
    }
    public void EndOfEnemyTurnDiceEfects(List<Die> specialDice)
    {
        foreach (var die in specialDice)
            effectApllier.ApplyEffect(die.DieData.faces[die.currentFace.normalValue-1].effectData);
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
    void Win( )
    {
        SoundAudioClip.instance.Destroymusic();
        SoundManager.PlayMusic(SoundManager.Sound.VictoryMusic, false);
        enemy.Defeted.RemoveListener(Win);
        string rewardText="";
        
        foreach (EffectData effect in enemy.rewards )
        {
            effectApllier.ApplyEffect(effect);
            rewardText += Log.Logs.Last().GetComponent<TextMeshProUGUI>().text +" " ;
        }
        win.Invoke(rewardText);
    }
    void Loss()
    {
        SoundAudioClip.instance.Destroymusic();
        SoundManager.PlayMusic(SoundManager.Sound.DefeatMusic, false);
        loss.Invoke();
    }
    void ApllyDiceEffects(List<Die> dice, Fighter resiver, Fighter dealer)
    {
        string sucesos;
        sucesos = "El " + dealer.name;
        foreach (Die die in dice)
        {
            effectApllier.ApplyEffect(die.currentFace.effect.effectData);
        }
        if(dice.Count > 0)
            TextoDeEffectos.Invoke(sucesos);
    }
}
