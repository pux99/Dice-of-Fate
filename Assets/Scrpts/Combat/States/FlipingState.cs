using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlipingState : CombatState
{
    [SerializeField]private int flips;
    public int FlipCount
    {
        get { return flips; }
    }
    public UnityEvent endOfFlip;
    public UnityEvent<int> FlipCountChange;

    private void Awake()
    {
        endOfFlip=new UnityEvent();
        FlipCountChange = new UnityEvent<int>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if(onPlace)
            {
                if (flips <= 0)
                {
                    active = false;
                    foreach (var die in dieList)
                    {
                        die.flippable = false;
                        die.flipt.RemoveListener(diceFlipt);
                    }
                    clearList();
                    endOfFlip.Invoke();
                    onPlace=false;
                }
            }
            else
            {
                MoveToContainer(15);
            }
        }   
    }
    public override void startState(List<Die> list,int value)
    {
        base.startState(list);
        flips=value;
        foreach (var die in dieList)
        {
            die.flippable=true;
            die.flipt.AddListener(diceFlipt);
        }
    }
    void diceFlipt()
    {
        flips--;
        FlipCountChange.Invoke(flips);
    }
    public void ModiffyFlipCount(int value)
    {
        flips += value;
        FlipCountChange.Invoke(flips);
    }

    
}
