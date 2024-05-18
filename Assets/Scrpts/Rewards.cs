using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


//[CustomEditor(typeof(Rewards))]
//public class testy : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        Rewards rewards = (Rewards)target;
//        EditorGUILayout.BeginHorizontal();
//        rewards.Face1 = (reward)EditorGUILayout.EnumPopup("a", rewards.Face1);
//        rewards.Face1 = (reward)EditorGUILayout.EnumPopup("a", rewards.Face2);
//        EditorGUILayout.EndHorizontal();

//    }
//}
[Serializable]
public class Rewards
{
     public enum EffectType
     {
         none,
         Heal,
         Damage,
         MaxLife,
         DiceMode,
         changaDie
     }
    [Serializable]
    public struct Effect
     {
         [SerializeField] private EffectType _reward;
         [SerializeField] private int _value;
        [SerializeField] private GameObject _GameObjectReward;
        public EffectType reward { get { return _reward; } }
         public int value { get { return _value; } }
        public GameObject GameObjectReward { get { return _GameObjectReward; } }
     }
     [Serializable]
     public struct Reward
     {
         public List<Effect> effects;
         [SerializeField] private string _consequence;
         public string consequence { get { return _consequence; } }
     }
     public Reward noRoll;
     public Reward roll1;
     public Reward roll2;
     public Reward roll3;
     public Reward roll4;
     public Reward roll5;
     public Reward roll6;
     
}

    
