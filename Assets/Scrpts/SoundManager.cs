using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    //Lista de los sonidos que se pueden usar.
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
    
    
    //Esta funcion recibe un sonido de la lista Sound y un booleano para decidir si el sonido se loopea o no. Luego crea un gameobject con el AudioSource y lo corre.
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
