using System;
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
        OnUseDie.Clear();
        foreach (var die in player.dice)
        {
            OnUseDie.Add(die);
        }
        player.OnStartOfBattle();
        enemy.OnStartOfBattle();
        player.OnTurnStart();
        combatStart.Invoke();
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
        DamageFigther(enemy,scoring.score);
        scoring.score = 0;
        scoring.TotalPointsChange.Invoke(scoring.score);
        ApllyDiceEffects(scoring.SpecialDice, enemy,player);
        foreach (Die die in player.dice)
        {
            die.Disolv(true);
        }
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
        }
        enemy.OnTurnStart();
    }
    public void EndOfEnemyTurn(int value)
    {
        player.Damage(value);
        player.OnTurnStart();
        //applyEffectsEnemy(enemy._OnTurnStartEffects);
    }
    public void EndOfEnemyTurnDiceEfects(List<Die> specialDice)
    {
        foreach (var die in specialDice)
            effectApllier.ApplyEffect(die.DieData.faces[die.currentFace.normalValue-1].effectData);
        //ApllyDiceEffects(specialDice, player, enemy);
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
    void Win()
    {
        enemy.Defeted.RemoveListener(Win);
        string rewardText="";
        foreach (Rewards effect in enemy.rewards)
        {
            applyEffects(effect.noRoll.effects);
            rewardText += effect.noRoll.consequence;
        }
        
        win.Invoke(rewardText);
        Debug.Log("defeted");
    }
    void Loss()
    {
        loss.Invoke();
    }

    void applyEffects(List<EffectData> effects)
    {

        foreach (EffectData effect in effects)
        {
            switch (effect.type)
            {
                case EffectData.Type.Heal:
                    effectApllier.heal.ApplyEffect(player, effect.Value);
                    break;
                case EffectData.Type.Damage:
                    effectApllier.damage.ApplyEffect(player, effect.Value);
                    break;
                case EffectData.Type.MaxLife:
                    effectApllier.maxheathMod.ApplyEffect(player, effect.Value);
                    break;
                case EffectData.Type.DiceMode:
                    effectApllier.diceamountMod.ApplyEffect(player, effect.Value);
                    break;
                default:
                    break;
            }
        }
    }
    //void applyEffectsEnemy(List<EffectData> effects)
    //{

    //    foreach (EffectData effect in effects)
    //    {
    //        switch (effect.type)
    //        {
    //            case EffectData.Type.Heal:
    //                effectApllier.heal.ApplyEffect(enemy, effect.Value);
    //                break;
    //            case EffectData.Type.Damage:
    //                effectApllier.damage.ApplyEffect(player, effect.Value);
    //                break;
    //            case EffectData.Type.MaxLife:
    //                effectApllier.maxheathMod.ApplyEffect(enemy, effect.Value);
    //                break;
    //            case EffectData.Type.DiceMode:
    //                effectApllier.diceamountMod.ApplyEffect(enemy, effect.Value);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}
    void ApllyDiceEffects(List<Die> dice, Fighter resiver, Fighter dealer)
    {
        string sucesos;
        sucesos = "El " + dealer.name;
        foreach (Die die in dice)
        {
            switch (die.currentFace.effect.effectData.type)
            {
                case EffectData.Type.Heal:
                    dealer.Heal(die.currentFace.effect.effectData.Value);
                    sucesos += " se curo "+ die.currentFace.effect.effectData.type;
                    break;
                case EffectData.Type.Damage:
                    resiver.Damage(die.currentFace.effect.effectData.Value);
                    sucesos += " le hizo " + die.currentFace.effect.effectData.Value + " de daño a "+ resiver.name;
                    break;
                default:
                    break;
            }
        }
        if(dice.Count > 0)
            TextoDeEffectos.Invoke(sucesos);
    }
}
