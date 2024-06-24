using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayerOnStart : MonoBehaviour
{
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuMusic, true);
    }
}
