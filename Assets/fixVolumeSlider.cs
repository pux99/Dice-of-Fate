using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fixVolumeSlider : MonoBehaviour
{
    Slider slider;
    public enum musicOrSound
    {
        Music,
        sound
    }
    public musicOrSound sound;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (sound == musicOrSound.sound)
        {
            SoundManager.soundVolumeChange.AddListener(fixSlider);
        }
        else
        {
            SoundManager.musicVolumeChange.AddListener(fixSlider);
        }

    }
    public void fixSlider(float volumen)
    {
        slider.value = volumen;
    }
}
