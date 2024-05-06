using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public CardEvent.Options option;
    public bool RollFinish;
    public BoardManager boardManager;
    public void StartUpButton(CardEvent.Options opt)
    {
        option = opt;
    }

    public void ActivateButton()
    {

        boardManager.ResolveEvent(option);

    }
}
