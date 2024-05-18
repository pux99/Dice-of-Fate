using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDie : Effect
{

    public void ApplyEffect(Fighter figther, int Value, GameObject die)
    {
        GameObject newDie = Instantiate(die);
        newDie.transform.parent = figther.diceHolder.transform;
        figther.dice.Add(newDie.GetComponent<Die>());
        newDie.gameObject.SetActive(false);
        Die dieToDestroy = figther.diceHolder.transform.GetChild(0).gameObject.GetComponent<Die>();
        figther.dice.Remove(dieToDestroy);
        Destroy(dieToDestroy.gameObject);
    }

}
