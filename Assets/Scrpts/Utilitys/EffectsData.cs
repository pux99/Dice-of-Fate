using UnityEngine;
using UnityEditor;


[System.Serializable]
public struct EffectData
{
    public enum Target
    {
        None,
        Player,
        Enemy
    }
    public enum Type
    {
        None,
        Heal,
        Damage,
        MaxLife,
        DiceMode,
        changaDie,
        EnemylossTurn,
        RevelEnemyCard,
        BuffEnemyArmor,
        healthBoss,
        ArmorBoss,
        DiceBoss,
        StartBattle,
        multiplyDamage,
        extraLife
    }
    public Target target;
    public Type type;
    [Header("CondicionalValues")]
    public int Value;
    public EnemyCard enemy;
    public ScriptableDie specialDie;

}
#if(UNITY_EDITOR)
//[CustomEditor(typeof(effectData))]
//public class EffectDataEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        //var script = (effectData)target;//porque me fuerza a que sea monobehaivior
//        //if (script.type == effectData.Type.Heal)
//        //{
//        //    script.Value = EditorGUILayout.IntField(label: "Value", script.Value);
//        //}
//    }
//}
#endif


