using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDie : Effect
{
    public GameObject BaseDie;
    public void ApplyEffect(Fighter figther, int Value, ScriptableDie die)
    {
        GameObject newDie = Instantiate(BaseDie);
        Die dieScript = newDie.GetComponent<Die>();
        dieScript.ChangeDiePropertys(die);
        newDie.transform.parent = figther.diceHolder.transform;
        figther.dice.Add(dieScript);
        newDie.gameObject.SetActive(false);
        Die dieToDestroy = figther.diceHolder.transform.GetChild(0).gameObject.GetComponent<Die>();
        figther.dice.Remove(dieToDestroy);
        Destroy(dieToDestroy.gameObject);
    }

}
