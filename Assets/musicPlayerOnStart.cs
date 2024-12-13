using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayerOnStart : MonoBehaviour
{
    public SoundManager.Sound music;
    void Start()
    {
        SoundManager.PlayMusic(music, true);
    }
}
