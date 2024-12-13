using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    private Image blackScreen;
    private Color AlphaControler = new Color(0, 0, 0, 1);
    private float alphaValue;
    private float speed;
    private bool fade;
    private void Awake()
    {
        blackScreen = GetComponent<Image>();
        blackScreen.color= AlphaControler;
        fade = true;
        speed = -0.5f;
        alphaValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            alphaValue += speed*Time.deltaTime;
            AlphaControler.a = alphaValue;
            blackScreen.color= AlphaControler;
            if(blackScreen.color.a>1||blackScreen.color.a<0)
                fade = false;
        }
    }
    public void FadeIN(float Fspeed)
    {
        fade = true;
        speed = Fspeed;
        alphaValue = 0;
    }
    public void FadeOUT(float Fspeed)
    {
        fade = true;
        speed = -Fspeed;
        alphaValue = 1;
    }
}
