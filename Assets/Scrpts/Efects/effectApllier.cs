using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectApllier : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] public Enemy enemy;
    [SerializeField] public BoardManager boardManager;

    public Damage damage;
    public Heal heal;
    public DiceAmountMod diceamountMod;
    public MaxHeathMod maxheathMod;
    public ChangeDie changeDie;
    public SkipEnemyTurn skipEnemyTurn;
    public RevealEnemyCards revealEnemyCards;
    public ModifyBossShield modifyBossShield;
    public ModifyBossHealth modifyBossHealth;
    public ModifyBossDieCount modifyBossDieCount;
    public Add1ExtraLife add1ExtraLife;
    public StartEventCombat startEventCombat;
    

    public void ApplyEffect(EffectData effect)
    {
        Fighter targert;
        if (effect.target==EffectData.Target.Player)
            targert = player;
        else
            targert = enemy;


        switch (effect.type)
        {
            case EffectData.Type.Heal:
                heal.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.Damage:
                damage.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.MaxLife:
                maxheathMod.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.DiceMode:
                diceamountMod.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.changaDie:
                changeDie.ApplyEffect(targert, effect.Value, effect.specialDie);
                break;
            case EffectData.Type.EnemylossTurn:
                skipEnemyTurn.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.RevelEnemyCard:
                revealEnemyCards.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.ArmorBoss:
                modifyBossShield.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.healthBoss:
                modifyBossHealth.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.DiceBoss:
                modifyBossDieCount.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.extraLife:
                add1ExtraLife.ApplyEffect(targert, effect.Value);
                break;
            case EffectData.Type.StartBattle:
                startEventCombat.ApplyEffect(targert, effect.Value, effect.enemy);
                break;
            default:
                break;
        }
    }
}

