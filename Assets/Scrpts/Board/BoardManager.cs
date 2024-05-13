using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    List<BoardSpace> spaceList=new List<BoardSpace>();
    [SerializeField] private BoardSpace currentSpace;
    public UnityEvent<CardEvent> cardEvent=new UnityEvent<CardEvent>();
    public UnityEvent<Rewards.Reward> endOfEventRewardCalculation=new UnityEvent<Rewards.Reward>();
    public CombatManager combat;
    public Die die;
    public effectApllier Effect;
    public Player player;
    public Enemy enemy;
    public bool watingDie;
    private CardEvent.Options currentOption;
    public MoveCamera moveCamera;
    public Vector3 dieStartingPosition; 
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
        if (space.card.CardType == "Event"&&!space.Used)
        {
            foreach(BoardSpace boardSpace in spaceList)
            {
                boardSpace.EventOnGoing = true;
            }
            cardEvent.Invoke((CardEvent)space.card);
        }
        if (space.card.CardType == "Enemy" && !space.Used)
        {
            foreach (BoardSpace boardSpace in spaceList)
            {
                boardSpace.EventOnGoing = true;
            }
            moveCamera.MoveToDice();
            CardEnemy cardEnemy = (CardEnemy)space.card;
            cardEnemy.SetUpEnemy(enemy);

        }

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

    public void ResolveEvent(CardEvent.Options options)
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

    void applyEffects(List< Rewards.Effect> effects)
    {

        foreach (Rewards.Effect effect in effects)
        {
            switch (effect.reward)
            {
                case Rewards.EffectType.Heal:
                    Effect.heal.ApplyEffect(player,effect.value);
                    break;
                case Rewards.EffectType.Damage:
                    Effect.damage.ApplyEffect(player,effect.value);
                    break;
                case Rewards.EffectType.MaxLife:
                    Effect.maxheathMod.ApplyEffect(player,effect.value);
                    break;
                case Rewards.EffectType.DiceMode:
                    Effect.diceamountMod.ApplyEffect(player,effect.value);
                    break;
                default:
                    break;
            }
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
}
