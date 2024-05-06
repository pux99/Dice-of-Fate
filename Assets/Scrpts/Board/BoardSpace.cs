using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardSpace : MonoBehaviour
{
    [SerializeField] private List<BoardSpace> _spaceNextToMe=new List<BoardSpace>();
    [SerializeField] private BoardSpace _previewSpace;
    public bool Active;
    public bool ImNext;
    public bool Used;
    public bool EventOnGoing;
    #region materials
    public Material Blinking;
    public Material CardBack;
    public Material CardFront;
    #endregion
    public Card card;
    public MeshRenderer meshRenderer;
    public UnityEvent<BoardSpace> newCurrentSpace=new UnityEvent<BoardSpace>();
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            foreach (BoardSpace space in _spaceNextToMe)
            {
                space.ImNext = true;
                space._previewSpace = this;
            }
            meshRenderer.material = CardFront;
        }
        if (ImNext)
        {
            meshRenderer.material = Blinking;
        }
        if(!Active&&!ImNext) 
        {
            meshRenderer.material= CardBack;
        }
        if (Used)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    public void TurnoOfNetxSpaces()
    {
        foreach (BoardSpace space in _spaceNextToMe)
        {
            space.ImNext = false;
        }
    }
    private void OnMouseOver()
    {
        if(ImNext&&!EventOnGoing)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _previewSpace.Active = false;
                _previewSpace.TurnoOfNetxSpaces();
                Active = true;
                newCurrentSpace.Invoke(this);
            }
        }
    }
}
