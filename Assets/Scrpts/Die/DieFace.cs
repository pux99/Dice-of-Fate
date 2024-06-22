using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFace : MonoBehaviour
{
    [Serializable]
    public struct diceFaceEffect
    {
        [HideInInspector]public string name;
        public EffectData effectData;
    }
    [SerializeField]
    public diceFaceEffect effect;
    [SerializeField]
    private int _Value;
    [SerializeField] private int NormalValue;//el numero que deberia tener si es un dado normal
    public int normalValue { get { return NormalValue; } }
    private bool _onTop;
    [SerializeField]
    public bool special;
    [SerializeField]
    private Vector3 _rotation;

    public bool onTop{get { return _onTop; }}
    public int value {get { return _Value; }}
    public Vector3 rotation{ get { return _rotation; }}
    private void Update()
    {
        Debug.DrawLine(transform.position, transform.up * 1000, Color.green);
    }
    public void ChekIfOnTop()
    {
        int layerMask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit,Mathf.Infinity,layerMask ))
            _onTop = true;
        else
        {
            Debug.DrawLine(transform.position, transform.up*1000,Color.green);
            _onTop =false;
        }
    }
}
