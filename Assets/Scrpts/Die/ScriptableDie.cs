using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newDie", menuName = "Die")]
public class ScriptableDie : ScriptableObject
{
    public Texture texture;
    public DieFace.diceFaceEffect[] faces = new DieFace.diceFaceEffect[6];
    private void OnValidate()
    {
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].name="Face "+(i+1).ToString();
        }
    }
}
