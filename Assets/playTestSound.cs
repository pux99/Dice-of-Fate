using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playTestSound : MonoBehaviour
{
    public SoundAudioClip audioClip;
    public bool soundPlaing;
    float timer=0;
    private void Update()
    {
        if (soundPlaing)
            timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            audioClip.DestroySounds();
            soundPlaing = false;
            timer = 0;
        }    

    }
    public void TestSound()
    {
        if (!soundPlaing)
        { 
            SoundManager.PlaySound(SoundManager.Sound.DamageSoundA, false); 
            soundPlaing = true;
        }    
    }
}
