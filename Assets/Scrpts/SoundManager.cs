using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public static class SoundManager
{
    //Lista de los sonidos que se pueden usar.
    public static float soundLevel =0.1f;
    public static float musicLevel = 0.1f;
    public static List<AudioSource> sounds =new List<AudioSource>();
    public static List<AudioSource> music = new List<AudioSource>();
    public static UnityEvent<float> soundVolumeChange=new UnityEvent<float>();
    public static UnityEvent<float> musicVolumeChange = new UnityEvent<float>();
    public enum Sound
    {
        OneDiceRollA,
        OneDiceRollB,
        OneDiceRollC,
        OneDiceRollD,
        OneDiceRollE,
        OneDiceRollF,
        OneDiceRollG,
        MenuMusic,
        BossMusic,
        EnemyMusic,
        BackgroundMusic,
        VictoryMusic,
        DefeatMusic,
        DamageSoundA,
        DamageSoundB,
        DamageSoundC
    }
    
    
    //Esta funcion recibe un sonido de la lista Sound y un booleano para decidir si el sonido se loopea o no. Luego crea un gameobject con el AudioSource y lo corre.
    public static void PlaySound(Sound sound, bool loop)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = soundLevel;
        audioSource.tag = "Sound";
        if (loop)
        {
            audioSource.loop = true;
        }
        sounds.Add(audioSource);
        audioSource.Play();
    }

    public static void PlayMusic(Sound sound, bool loop)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = musicLevel;
        audioSource.tag = "Sound";
        if (loop)
        {
            audioSource.loop = true;
        }
        music.Add(audioSource);
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
