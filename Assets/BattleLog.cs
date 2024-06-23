using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BattleLog : MonoBehaviour
{
    public List<GameObject> Logs=new List<GameObject>();
    public GameObject baseLog;
    public GameObject log;
    public EffectApllier effectApllier;

    private void Start()
    {
        effectApllier.effectAplide.AddListener(AddEffectLog);
    }
    public void AddLog(string logText)
    {
        GameObject newlog= Instantiate(baseLog, log.transform);
        newlog.GetComponent<TextMeshProUGUI>().text = logText;
        Logs.Add(newlog);
    }
    public void AddEffectLog(EffectData effect,Fighter target)
    {
        GameObject newlog = Instantiate(baseLog,log.transform);
        string text;
        string color;
        if (effect.target == EffectData.Target.Player)
            color= "<color=#005500>";
        else
            color = "<color=#FF0000>";
        switch (effect.type)
        {
            case EffectData.Type.Heal:
                text = color+target.name+ "</color>" + " se curo "+effect.Value+" puntos de vida ";
                break;
            case EffectData.Type.Damage:
                text = color + target.name + "</color>" + " perdio " + effect.Value + " puntos de vida";
                break;
            case EffectData.Type.MaxLife:
                if(effect.Value>0)
                    text = color + target.name + "</color>" + " Gano " + effect.Value + " puntos de vida maxima ";
                else
                    text = color + target.name + "</color>" + " perdio " + Mathf.Abs( effect.Value) + " puntos de vida maxima ";
                break;
            case EffectData.Type.DiceMode:
                if (effect.Value > 0)
                {
                    if(effect.Value==1)
                        text = color + target.name + "</color>" + " Gano un dado";
                    else
                        text = color + target.name + "</color>" + " Gano " + effect.Value + " Dados ";
                }
                    
                else
                {
                    if (target.dice.Count > effect.Value * -1)
                    {
                        if (effect.Value == -1)
                            text = color + target.name + "</color>" + " perdio un dado ";
                        else
                            text = color + target.name + "</color>" + " perdio " + Mathf.Abs(effect.Value) + " Dados ";
                    }
                    else
                        text = "A " + color + target.name + "</color>" + " solo le queda un dado ";

                }
                break;
            case EffectData.Type.changaDie:
                text = color + target.name + "</color>" + " gano un dado especial";
                break;
            case EffectData.Type.EnemylossTurn:
                text = color + target.name + "</color>" + " perdera su proximo turno";
                break;
            case EffectData.Type.extraLife:
                text = color + target.name + "</color>" + " a ganado una vida extra";
                break;
            case EffectData.Type.StartBattle:
            default:
                text = "";
                break;
        }// texto
        newlog.GetComponent<TextMeshProUGUI>().text = text;
        Logs.Add(newlog);
    }
    public void ClearLogs()
    {
        for (int i = 0; i < Logs.Count; i++)
        {
            Destroy(Logs[i]);
        }
    }
}
