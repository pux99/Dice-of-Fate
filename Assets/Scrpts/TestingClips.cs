using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingClips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.PlaySound(SoundManager.Sound.OneDiceRollA);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.PlaySound(SoundManager.Sound.OneDiceRollA);
        }
    }

    
}
