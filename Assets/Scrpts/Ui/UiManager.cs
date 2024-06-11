using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



//                                                 //
//                                                 //
//                                                 //
//                                                 //
//                                                 //
//     Separar En varios UImanagers Espesificos    //
//                                                 //
//                                                 //
//                                                 //
//                                                 //
public class UiManager : MonoBehaviour
{
    [Serializable]
    public class MyButton
    {
        [SerializeField] public GameObject gameObject;
        [HideInInspector]public Button button;
        [HideInInspector] public TextMeshProUGUI text;
        public virtual void SetUp() {
            button=gameObject.GetComponent<Button>();
            text = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }
    [Serializable]
    public class MyOptionButton : MyButton
    {
        [HideInInspector] public OptionButton option;
        public override void SetUp()
        {
            base.SetUp();
            option = gameObject.GetComponent<OptionButton>();
        }
       
    }

    #region Combat
    [Header("Combat")]
    [SerializeField] private GameObject options;
    [SerializeField] private CombatManager combat;
    [SerializeField] private GameObject CombatUI;
    [SerializeField] private Slider playerHP;
    [SerializeField] private TextMeshProUGUI playerShield;
    [SerializeField] private Slider enemyHP;
    [SerializeField] private TextMeshProUGUI enemyShiel;
    [SerializeField] private Image EnemyCard;
    [SerializeField] private TextMeshProUGUI flips;//combertir botones en MYButton
    [SerializeField] private TextMeshProUGUI totalAP;
    [SerializeField] private TextMeshProUGUI currentAP;
    [SerializeField] private Button BRoll;
    [SerializeField] private Button BAddDice;
    [SerializeField] private Button BEndTurn;
    [SerializeField] private Button BRollTheRest;
    [SerializeField] private Button BPlusFlips;
    [SerializeField] private Button BMinusFlips;
    [SerializeField] private TextMeshProUGUI sucesos;

    [SerializeField] private GameObject WinOverlay;
    [SerializeField] private GameObject endOfGameOverlay;
    [SerializeField] private GameObject LossOverlay;
    #endregion
    #region Board
    [Space(2)]
    [Header("Board")]
    [SerializeField] private BoardManager board;
    [SerializeField] private GameObject BoardUI;
    [SerializeField] private Image CardImage;
    [SerializeField] private TextMeshProUGUI EventText;
    [SerializeField] private MyOptionButton option1;
    [SerializeField] private MyOptionButton option2;
    [SerializeField] private MyOptionButton option3;
    [SerializeField] private Button BEndEvent;
    
    #endregion






    void Start()
    {
        combat.combatStart.AddListener(CombatStart);
        BoardEventSubscription();
        BoraedSetUp();
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(options.active)
            {
                options.SetActive(false);
            }
            else
                options.SetActive(true);

            
        }
        if (combat.pOnUseDie.Count <= 0)
            BRollTheRest.interactable = false;
    }
    void CombatStart()
    {
        CombatUI.SetActive(true);
        EnemyCard.sprite = combat.enemy.card.CardArt;
        combat.flip.FlipCountChange.AddListener(modifyFlips);
        CombatEventSubscription();
        MakeButtonsNotInteractable();
        RollingStatebuttons();
        inicialSetup();

        
    }
    void BoraedSetUp()
    {
        option1.SetUp();
        option2.SetUp();
        option3.SetUp();
    }
    void battleEnd() 
    {
        CombatUI.SetActive(false);
    }

    public void CombatEventSubscription()
    {
        combat.flip.endOfFlip.AddListener(SelectionStatebuttons);
        combat.rolling.endOfRoling.AddListener(flipStatebuttons);
        combat.select.SelectedPointsChange.AddListener(ChangeCurrentScore);
        combat.scoring.TotalPointsChange.AddListener(TotalCurrentScore);
        combat.select.ScoringEvent.AddListener(turnOnRollAgain);
        combat.enemyTurn.EndOfEnemyTurn.AddListener(NewTurnButtons);
        combat.combatStart.AddListener(inicialSetup);
        combat.enemy.UpdateHealthBar.AddListener(UpdateHP);
        combat.player.UpdateHealthBar.AddListener(UpdateHP);
        combat.FlipValueChange.AddListener(FlipValueChange);
        combat.win.AddListener(WinCombat);
        combat.loss.AddListener(LossCombat);
        combat.TextoDeEffectos.AddListener(CombatEffectText);
    }
    public void BoardEventSubscription() 
    {
        board.cardEvent.AddListener(CardEventDisplay);
        board.endOfEventRewardCalculation.AddListener(CardEventEnding);
    }


    void inicialSetup()
    {
        enemyShiel.text = combat.enemy.shield.ToString();
        playerShield.text = combat.player.shield.ToString();
    }
    private void modifyFlips(int value)
    {
        flips.text = value.ToString();
    }
    public void Roll()
    {
        combat.ResetAndRoll();
        MakeButtonsNotInteractable();
        sucesos.gameObject.SetActive(false);
    }
    public void RollTheRest()
    {
        if (combat.select.pointsScored)
        {
            combat.RollTheRest();
            MakeButtonsNotInteractable();
        }
    }
    public void EndOfTurn()
    {
        MakeButtonsNotInteractable();
        combat.EndOfPlayerTurn();
        
    }
    public void MakeButtonsNotInteractable()
    {
        BRoll.interactable = false;
        BEndTurn.interactable = false;
        BAddDice.interactable = false;
        BRollTheRest.interactable = false;
        BPlusFlips.interactable = false;
        BMinusFlips.interactable = false;

    }
    private void flipStatebuttons() 
    {
        MakeButtonsNotInteractable();
    }
    private void SelectionStatebuttons()
    {
        BRoll.interactable = false;
        BAddDice.interactable = true;
        BEndTurn.interactable= true;
        BRollTheRest.interactable = false;
        BPlusFlips.interactable = true;
        BMinusFlips.interactable = true;
    }
    private void RollingStatebuttons()
    {
        BRoll.interactable = true;
        BPlusFlips.interactable = true;
        BMinusFlips.interactable = true;
    }
    private void NewTurnButtons(int value) 
    {
        BRoll.interactable = true;
        BPlusFlips.interactable = true;
        BMinusFlips.interactable = true;
        BAddDice.interactable = false;
        BEndTurn.interactable = false;
        BRollTheRest.interactable = false;
    }
    public void ChangeCurrentScore(int value)
    {
        currentAP.text=value.ToString();
    }
    public void TotalCurrentScore(int value)
    {
        totalAP.text = value.ToString();
    }
    public void UpdateHP(Fighter fighter)
    {
        if (fighter == combat.player)
            playerHP.value = (float)fighter.health / (float)fighter.maxHealth;
        else enemyHP.value = (float)fighter.health / (float)fighter.maxHealth;
    }
    void FlipValueChange(int value)
    {
        flips.text = value.ToString();
    }
    void turnOnRollAgain(List<Die> list)
    {
        if(combat.pOnUseDie.Count > 0)
            BRollTheRest.interactable = true;
    }
    void CardEventDisplay(ScriptableEventCard card)
    {
        BoardUI.SetActive(true);
        EventText.text = card.cardText;
        CardImage.sprite = card.CardArt;
        if (card.options.Count > 0)
        {
            option1.option.option = card.options[0];
            option1.gameObject.SetActive(true);
            option1.text.text = card.options[0].buttonText;
            option1.button.interactable = true;
        }
        else
            option1.gameObject.SetActive(false);
        if (card.options.Count > 1)
        {
            option2.option.option = card.options[1];
            option2.gameObject.SetActive(true);
            option2.text.text = card.options[1].buttonText;
            option2.button.interactable = true;
        }
        else
            option2.gameObject.SetActive(false);
        if (card.options.Count > 2)
        {
            option3.option.option = card.options[2];
            option3.gameObject.SetActive(true);
            option3.text.text = card.options[2].buttonText;
            option3.button.interactable = true;
        }
        else
            option3.gameObject.SetActive(false);
    }
    public void CardEventEnding(Rewards.Reward reward)
    {
        EventText.text = EventText.text + "\n" + reward.consequence;
        option1.button.interactable = false;
        option2.button.interactable = false;
        option3.button.interactable = false;
        BEndEvent.gameObject.SetActive(true);
        
    }
    public void EventEnding()
    {

        BoardUI.SetActive(false);
        BEndEvent.gameObject.SetActive(false);
        board.ResumeBoardMovement();
    }
    public void WinCombat(string Reward)
    {
        CombatUI.SetActive(false);
        WinOverlay.SetActive(true);
        WinOverlay.transform.Find("Reward").GetComponent<TextMeshProUGUI>().text = Reward;
        if (board.PCurrentSpace.finalSpace)
        {
            WinOverlay.SetActive(false);
            endOfGameOverlay.SetActive(true);
        }
    }
    public void LossCombat()
    {
        CombatUI.SetActive(false);
        LossOverlay.SetActive(true);
    }

    public void CombatEffectText(string text) 
    {
        sucesos.gameObject.SetActive(true);
        sucesos.text = text;
    }
   
}
