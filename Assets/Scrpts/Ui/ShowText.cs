using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public class TextToShow
    {
        public float alphaSpeed;
        public TextMeshProUGUI text;
        public bool show;
        public bool a;
    }
    private List<TextToShow> text =new List<TextToShow>(); 
    void Update()
    {
        for (int i = 0; i < text.Count; i++)    
        {
            if (text[i].show)
                if (text[i].text.alpha <= 1)
                    text[i].text.alpha += text[i].alphaSpeed*Time.deltaTime;
                else
                {
                    text.Remove(text[i]);
                    i--;
                }
        }
        
    }

    public void Show(TextMeshProUGUI Ttext,float Tspeed)
    {
        TextToShow nText = new TextToShow();
        nText.text = Ttext;
        nText.alphaSpeed = Tspeed;
        nText.text.alpha = 0;
        nText.show = true;
        text.Add(nText);
    }
}
