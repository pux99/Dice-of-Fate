using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<BoardSpace> spaceList=new List<BoardSpace>();
    [SerializeField] private BoardSpace currentSpace;
    public UnityEvent<EventCard> cardEvent=new UnityEvent<EventCard>();
    public UnityEvent<Rewards.Reward> endOfEventRewardCalculation=new UnityEvent<Rewards.Reward>();
    public CombatManager combat;
    public Die die;
    public EffectApllier effectApllier;
    public Player player;
    public Enemy enemy;
    public bool watingDie;
    private EventCard.Options currentOption;
    public MoveCamera moveCamera;
    public Vector3 dieStartingPosition;
    public BossModifiers bossMods=new BossModifiers();

    public BoardSpace PCurrentSpace
    {
        get { return currentSpace; }
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<BoardSpace>() != null)
                spaceList.Add(transform.GetChild(i).GetComponent<BoardSpace>());
        }
        foreach (BoardSpace space in spaceList)
        {
            space.newCurrentSpace.AddListener(changeCurrentSpace);
        }
        moveCamera.inPositionDice.AddListener(StartCombat);
    }

    // Update is called once per frame
    void Update()
    {
        currentSpace.Active = true;
        if (watingDie)
        {
            if (die.stopRolling)
            {
                switch (die.value)
                {
                    case 1:
                        applyEffects(currentOption.options.roll1.effects);
                        endOfEventRewardCalculation.Invoke(currentOption.options.roll1);
                        break;
                    case 2:
                        applyEffects(currentOption.options.roll2.effects);
                        endOfEventRewardCalculation.Invoke(currentOption.options.roll2);
                        break;
                    case 3:
                        applyEffects(currentOption.options.roll3.effects);
                        endOfEventRewardCalculation.Invoke(currentOption.options.roll3);
                        break;
                    case 4:
                        applyEffects(currentOption.options.roll4.effects);
                        endOfEventRewardCalculation.Invoke(currentOption.options.roll4);
                        break;
                    case 5:
                        applyEffects(currentOption.options.roll5.effects);
                        endOfEventRewardCalculation.Invoke(currentOption.options.roll5);
                        break;
                    case 6:
                        applyEffects(currentOption.options.roll6.effects);
                        endOfEventRewardCalculation.Invoke(currentOption.options.roll6);
                        break;
                    default: break;
                }
                watingDie = false;
            }
        }
    }
    void changeCurrentSpace(BoardSpace space)
    {
        currentSpace= space;
        if (space.card.GetType() == typeof(EventCard)&&!space.Used)//space.card.CardType == "Event"&&!space.Used)
        {
            foreach(BoardSpace boardSpace in spaceList)
            {
                boardSpace.EventOnGoing = true;
            }
            cardEvent.Invoke((EventCard)space.card);
        }
        if (space.card.GetType() == typeof(EnemyCard) && !space.Used)//space.card.CardType == "Enemy" && !space.Used)
        {
            startCombat((EnemyCard)space.card);
            //foreach (BoardSpace boardSpace in spaceList)
            //{
            //    boardSpace.EventOnGoing = true;
            //}
            //moveCamera.MoveToDice();
            //EnemyCard cardEnemy = (EnemyCard)space.card;
            //cardEnemy.SetUpEnemy(enemy,bossMods);

        }
        space.Used = true;

    }
    public void CombatEnd()
    {
        foreach (BoardSpace boardSpace in spaceList)
        {
            boardSpace.EventOnGoing = false;
        }
    }

    void StartCombat()
    {
        combat.CombatStart(player, enemy);
    }

    public void ResolveEvent(EventCard.Options options)
    {
        currentOption = options;
        if (options.roll)
        {
            die.Disolv(false);
            die.transform.position = dieStartingPosition;
            die.Roll();
            watingDie = true;
        }
        else
        {
            applyEffects(options.options.noRoll.effects);
            endOfEventRewardCalculation.Invoke(currentOption.options.noRoll);
            //options.options.noRoll.consequence
        }
    }

    void applyEffects(List< EffectData> effects)
    {

        foreach (EffectData effect in effects)
        {
            effectApllier.ApplyEffect(effect);
        //    switch (effect.type)
        //    {
        //        case EffectData.Type.Heal:
        //            effectApllier.heal.ApplyEffect(player,effect.Value);
        //            break;
        //        case EffectData.Type.Damage:
        //            effectApllier.damage.ApplyEffect(player,effect.Value);
        //            break;
        //        case EffectData.Type.MaxLife:
        //            effectApllier.maxheathMod.ApplyEffect(player,effect.Value);
        //            break;
        //        case EffectData.Type.DiceMode:
        //            effectApllier.diceamountMod.ApplyEffect(player,effect.Value);
        //            break;
        //        case EffectData.Type.changaDie:
        //            effectApllier.changeDie.ApplyEffect(player, effect.Value, effect.specialDie);
        //            break;
        //        case EffectData.Type.EnemylossTurn:
        //            effectApllier.skipEnemyTurn.ApplyEffect(enemy, effect.Value);
        //            break;
        //        case EffectData.Type.RevelEnemyCard:
        //            effectApllier.revealEnemyCards.ApplyEffect(player, effect.Value);
        //            break;
        //        case EffectData.Type.ArmorBoss:
        //            effectApllier.modifyBossShield.ApplyEffect(player, effect.Value);
        //            break;
        //        case EffectData.Type.healthBoss:
        //            effectApllier.modifyBossHealth.ApplyEffect(player, effect.Value);
        //            break;
        //        case EffectData.Type.DiceBoss:
        //            effectApllier.modifyBossDieCount.ApplyEffect(player, effect.Value);
        //            break;
        //        case EffectData.Type.extraLife:
        //            effectApllier.add1ExtraLife.ApplyEffect(player, effect.Value);
        //            break;
        //        case EffectData.Type.StartBattle:
        //            effectApllier.startEventCombat.ApplyEffect(player, effect.Value,effect.enemy);
        //            break;
        //        default:
        //            break;
        //    }
        }
    }
    public void ResumeBoardMovement()
    {
        currentSpace.Used = true;
        foreach (BoardSpace boardSpace in spaceList)
        {
            boardSpace.EventOnGoing = false;
        }
    }
    public void reveleEnemyCard()
    {
        foreach (BoardSpace boardSpace in spaceList) 
        {
            if(boardSpace.card!=null&&boardSpace.card.GetType() == typeof(EnemyCard))
                boardSpace.reveld=true;

        }
    }
    public void startCombat(EnemyCard enemyCard)
    {
        foreach (BoardSpace boardSpace in spaceList)
        {
            boardSpace.EventOnGoing = true;
        }
        moveCamera.MoveToDice();
        enemyCard.SetUpEnemy(enemy, bossMods);
    }
}
