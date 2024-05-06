using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    #region Variables
    private List<DieFace> _faces=new List<DieFace>();
    private GameObject _outline;

    public UnityEvent flipt;
    public UnityEvent<bool,Die> select;


    [SerializeField] private DieFace _currentFace;
    [SerializeField] private Rigidbody rb;


    private int _value;
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
    }
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        for(int i = 0; i < transform.childCount-1; i++) 
        {
            _faces.Add(transform.GetChild(i).GetComponent<DieFace>());
        }
        _outline= transform.GetChild(transform.childCount - 1).gameObject;
        freez();
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
    }
    void RollingPhase()
    {
        if (rb.velocity.magnitude <= 0.01)
        {
            freez();
            foreach(DieFace face in _faces)
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
}
