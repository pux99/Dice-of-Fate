using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public EventCard.Options option;
    public bool RollFinish;
    public BoardManager boardManager;
    public void StartUpButton(EventCard.Options opt)
    {
        option = opt;
    }

    public void ActivateButton()
    {

        boardManager.ResolveEvent(option);

    }
}
