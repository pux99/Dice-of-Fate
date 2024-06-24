using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAudioClip: MonoBehaviour
{
    [System.Serializable]
    public class SoundAudioClipClass
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    List<AudioSource> sounds = new List<AudioSource>();

    // Esta variable esta para poder pasarle el array a funciones statics.
    private static SoundAudioClip _instance;
    public static SoundAudioClip instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<SoundAudioClip>("SoundManager"));
            }
            return _instance;
        }
    }

    //Esta funcion busca todos los objetos con el tag "Sound" y los destruye.
    public void DestroySounds()
    {
        foreach (AudioSource audioSource in SoundManager.sounds)
        {
            Destroy(audioSource.gameObject);
        }
        SoundManager.sounds.Clear();
    }

    public SoundAudioClipClass[] soundAudioClips;

    public void ChangeVolume(float volume)
    {
        SoundManager.soundLevel = volume;
        foreach (AudioSource audioSource in SoundManager.sounds)
        {
            audioSource.volume = volume;
        }
    }
}
