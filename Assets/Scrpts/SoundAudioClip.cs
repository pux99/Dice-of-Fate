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

    public void DestroySounds()
    {
        GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sound");
        foreach (GameObject audioSource in sounds)
        {
            Destroy(audioSource);
        }
    }

    public SoundAudioClipClass[] soundAudioClips;
}
