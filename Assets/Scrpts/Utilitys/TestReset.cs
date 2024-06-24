using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestReset : MonoBehaviour
{
    // Start is called before the first frame update
    public void resetLevel()
    {
        SceneManager.LoadScene(0);
        SoundAudioClip.instance.DestroySounds();
        SoundAudioClip.instance.Destroymusic();
    }
}
