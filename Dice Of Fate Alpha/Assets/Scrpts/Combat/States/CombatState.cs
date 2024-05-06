using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatState : MonoBehaviour
{
    protected List<Die> dieList = new List<Die>();
    public GameObject container;
    protected bool onPlace;
    protected bool active;
    public void fillList(List<Die> list)
    {
        foreach (Die die in list) 
        {
            dieList.Add( die );
        }
    }
    protected void removeFormList(Die die)
    {
        dieList.Remove( die );
    }
    protected void removeFormList(List<Die> list)
    {
        foreach(Die die in list)
        { 
            dieList.Remove( die );
        }
    }
    protected void clearList()
    {
        dieList.Clear();
    }
    public virtual void startState(List<Die> list)
    {
        active = true;
        fillList(list);
    }
    public virtual void startState(List<Die> list,int value)
    {
        startState(list);
    }
    protected void MoveToContainer( float speed) 
    {
        List<Die> list = dieList;
        if (list.Count > 0)
        {
            float DieSize = list[0].size * 2;
            int inCorectPlase = 0;
            Vector3 offset = new Vector3(-container.transform.localScale.x, 0, container.transform.localScale.z);

            for (int i = 0; i < list.Count; i++)
            {
                list[i].freez();//freeze constrains
                list[i].transform.position = Vector3.MoveTowards(list[i].transform.position,
                                                                    container.transform.position + offset * DieSize / 2 + new Vector3((int)(i / 3) * (DieSize + 0.3f), 0, -(i % 3 * (DieSize + 0.3f))),
                                                                    speed * Time.deltaTime);//move to new position
                if (list[i].transform.position == container.transform.position + offset * DieSize / 2 + new Vector3((int)(i / 3) * (DieSize + 0.3f), 0, -(i % 3 * (DieSize + 0.3f))))
                {
                    inCorectPlase++;//+1 is a die is in position
                }
            }
            if (inCorectPlase == list.Count)//check if all dice ar in position
            {
                onPlace = true;
            }
        }
    }
}
