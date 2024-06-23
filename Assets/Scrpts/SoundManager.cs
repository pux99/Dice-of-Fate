using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        OneDiceRollA,
        OneDiceRollB,
        OneDiceRollC,
        OneDiceRollD,
        OneDiceRollE,
        OneDiceRollF,
        OneDiceRollG,
    }
    
    
    public static void PlaySound(Sound sound, bool loop)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.tag = "Sound";
        if (loop)
        {
            audioSource.loop = true;
        }
        audioSource.Play();

    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAudioClip.SoundAudioClipClass soundAudioClipNow in SoundAudioClip.instance.soundAudioClips)
        {
            if (soundAudioClipNow.sound == sound)
            {
                return soundAudioClipNow.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found.");
        return null;
    }


}
