using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGlow : MonoBehaviour
{
    public Button buton;
    public Image glow;
    public float speed;
    public float maxValue;
    private bool ascendiong;

    private void Update()
    {
        if (buton.interactable)
        {
            if (glow.color.a >= maxValue)
            {
                ascendiong = false;
            }
            if (glow.color.a <= 0)
            {
                ascendiong=true;
            }
            if (ascendiong)
                glow.color = new Color(glow.color.r, glow.color.g, glow.color.b, glow.color.a + speed * Time.deltaTime);
            else
                glow.color = new Color(glow.color.r, glow.color.g, glow.color.b, glow.color.a - speed * Time.deltaTime);
        }
        else
            glow.color = new Color(glow.color.r, glow.color.g, glow.color.b, 0);


    }
}
