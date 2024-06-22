using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    #region Variables
    [SerializeField] public ScriptableDie DieData;

    private List<DieFace> _faces=new List<DieFace>();
    private GameObject _outline;

    public UnityEvent flipt;
    public UnityEvent<bool,Die> select;


    [SerializeField] private DieFace _currentFace;
    public DieFace currentFace {  get { return _currentFace; } }
    [SerializeField] private Rigidbody rb;

    private bool Disolving;
    private float disolv;
    public float disolvTarget;
    [SerializeField] private float disolvSpeed;
    private MeshRenderer mr;

    [SerializeField]private int _value;
    private bool _rolling;
    private bool _stopRolling;
    public bool selectable;
    public bool flippable;
    private bool _selected;
    [SerializeField]private float _size;


    public float size
    {
        get { return _size; }
    }
    public int value
    {
        get { return _value; }
    }
    public bool selected
    {
        get { return _selected; }
    }
    public bool stopRolling
    {
        get { return _stopRolling; }
    }

    public bool testButton;
    #endregion
    private void Awake()
    {
        flipt=new UnityEvent();
        select=new UnityEvent<bool,Die>();
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount-1; i++) 
        {
            _faces.Add(transform.GetChild(i).GetComponent<DieFace>());
        }
        if (DieData != null)
        {
            mr.material.mainTexture = DieData.texture;
            for (int i = 0; i < _faces.Count; i++)
            {
                _faces[i].effect = DieData.faces[i];
                if (_faces[i].effect.effectData.type != EffectData.Type.None)
                    _faces[i].special = true;
            }
        }
        _outline = transform.GetChild(transform.childCount - 1).gameObject;
        freez();

        disolv = 1;
        foreach (Material mat in mr.materials)
        {
            mat.SetFloat("_Disolv", disolv);
        }
    }

    void Update()
    {
        if(rb.velocity.magnitude > 0)
        {
            _rolling = true;
        }
        if (_rolling)
        {
            RollingPhase();
        }
        if (Disolving )
        {
            if (disolvTarget == 1)
            {
                disolv += disolvSpeed * Time.deltaTime;
                if (disolv >= 1)
                {
                    Disolving = false;
                }
            }
            else
            {
                disolv -= disolvSpeed * Time.deltaTime;
                if (disolv <= 0)
                {
                    Disolving = false;
                }
            }
            foreach (Material mat in mr.materials)
            {
                mat.SetFloat("_Disolv", disolv);
            }
        }  
    }
    void RollingPhase()
    {
        if (rb.velocity.magnitude <= 0.01)
        {
            CheckValue();
        }
    }
    public void CheckValue()
    {
        freez();
        foreach (DieFace face in _faces)
        {
            face.ChekIfOnTop();
            if (face.onTop)
                _currentFace = face;
        }
        _value = _currentFace.value;
        fixRotation();
        _rolling = false;
        _stopRolling = true;
    }
    public void Roll()
    {
        UnFreez();
        rb.AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-20, -10), Random.Range(10, 20)), ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.Range(30, 100), Random.Range(30, 50), Random.Range(30, 200)), ForceMode.Impulse);
        _stopRolling = false;
    }
    void fixRotation()
    {
        transform.rotation = Quaternion.Euler(_currentFace.rotation);
    }
    public void flip()
    {
        _currentFace = _faces[5 - _faces.IndexOf(_currentFace)];
        _value = _currentFace.value;
        fixRotation();
        flippable = false;
    }

    public void freez()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    void UnFreez()
    {
        rb.WakeUp();
        rb.constraints = RigidbodyConstraints.None;
    }

    public void turnOffOutline()
    {
        _outline.SetActive(false);
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer==7)
            rb.AddForce((gameObject.transform.position- collision.gameObject.transform.position)*.1f, ForceMode.Force);
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectable)
            {
                _selected = !_selected;
                _outline.SetActive(_selected);
            }
            select.Invoke(_selected,this);
        }//selecting

        if (Input.GetMouseButtonDown(1))
        {
            if (flippable)
            {
                
                flip();
                flipt.Invoke();
            }
        }//fliping
    }
    public void Disolv(bool OnOff)
    {
        Disolving = true;
        if (OnOff)
        {
            disolvTarget = 1;
        }
        else
        {
            disolvTarget=0;
        }
    }
    public void Randomize()
    {
        transform.Rotate(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360));
    }
    public void GetReadyToRoll()
    {
        _stopRolling = false;
    }
    public void ChangeDiePropertys(ScriptableDie Data)
    {
        DieData = Data;
        if (DieData != null)
        {
            mr.material.mainTexture = DieData.texture;
            for (int i = 0; i < _faces.Count; i++)
            {
                _faces[i].effect = DieData.faces[i];
                if(_faces[i].effect.effectData.type!=EffectData.Type.None)
                    _faces[i].special=true;
            }
        }
    }
}
