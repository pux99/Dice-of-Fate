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
                _instance = Instantiate(Resources.Load<SoundAudioClip>("SoundAudioClip"));
            }
            return _instance;
        }
    }


    public SoundAudioClipClass[] soundAudioClips;
}
