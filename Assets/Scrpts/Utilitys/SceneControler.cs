using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{

    public void sceneChange(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
        SoundAudioClip.instance.Destroymusic();
        SoundAudioClip.instance.DestroySounds();
    }
    public void restartTime()
    {
        
    }
    
}
