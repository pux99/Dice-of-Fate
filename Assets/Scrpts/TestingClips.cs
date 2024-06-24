using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingClips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.PlaySound(SoundManager.Sound.OneDiceRollA);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollA, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollB, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollC, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollD, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollE, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollF, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollG, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SoundAudioClip.instance.DestroySounds();
        }
    }

    
}
