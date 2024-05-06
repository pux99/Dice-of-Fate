using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFace : MonoBehaviour
{
    [SerializeField]
    private int _Value;
    private bool _onTop;
    [SerializeField]
    private Vector3 _rotation;

    public bool onTop
    {
        get { return _onTop; }
    }
    public int value 
    {
        get { return _Value; }
    }
    public Vector3 rotation
    {
        get { return _rotation; }
    }
    public void ChekIfOnTop()
    {
        int layerMask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit,Mathf.Infinity,layerMask ))
        {
            _onTop = true;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.up*1000,Color.green);
            _onTop =false;
        }
    }
}
