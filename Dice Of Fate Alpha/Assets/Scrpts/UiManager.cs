using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private CombatManager combat;
    [SerializeField] private Slider playerHP;
    [SerializeField] private TextMeshProUGUI playerShield;
    [SerializeField] private Slider enemyHP;
    [SerializeField] private TextMeshProUGUI enemyShiel;
    [SerializeField] private Image EnemyCard;
    [SerializeField] private TextMeshProUGUI flips;
    [SerializeField] private TextMeshProUGUI totalAP;
    [SerializeField] private TextMeshProUGUI currentAP;
    [SerializeField] private Button BRoll;
    [SerializeField] private Button BAddDice;
    [SerializeField] private Button BEndTurn;
    [SerializeField] private Button BRollTheRest;
    [SerializeField] private Button BPlusFlips;
    [SerializeField] private Button BMinusFlips;



    void Start()
    {
        combat.flip.FlipCountChange.AddListener(modifyFlips);
        EventSubscription();
        MakeButtonsNotInteractable();
        RollingStatebuttons();
        inicialSetup();
    }

    public void EventSubscription()
    {
        combat.flip.endOfFlip.AddListener(SelectionStatebuttons);
        combat.rolling.endOfRoling.AddListener(flipStatebuttons);
        combat.select.SelectedPointsChange.AddListener(ChangeCurrentScore);
        combat.scoring.TotalPointsChange.AddListener(TotalCurrentScore);
        combat.enemyTurn.EndOfEnemyTurn.AddListener(NewTurnButtons);
        combat.combatStart.AddListener(inicialSetup);
        combat.enemy.UpdateHealthBar.AddListener(UpdateHP);
        combat.player.UpdateHealthBar.AddListener(UpdateHP);
        combat.FlipValueChange.AddListener(FlipValueChange);
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
    }
    public void RollTheRest()
    {
        combat.RollTheRest();
        MakeButtonsNotInteractable();
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
        BRollTheRest.interactable = true;
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
}
