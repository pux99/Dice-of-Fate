using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComabtStarter : MonoBehaviour
{
    public CombatManager combatManager;
    public Enemy enemy;
    public Player player;
    public bool button;
    // Start is called before the first frame update
    void Start()
    {
    }

    
    // Update is called once per frame
    void Update()
    {
        if (button)
        {
            combatManager.CombatStart(player, enemy);
            button = false;
        }
    }
}
