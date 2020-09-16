using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,Character
{
    
    public int hp = 5;
    private bool combatant = false;
    public Enemy(int hp)
    {
        this.hp = hp;
    }
    public void enterCombat()
    {
        this.combatant = true;
    }
    public void exitCombat()
    {
        this.combatant = false;
    }
    public bool InCombat()
    {
        return combatant;
    }
}
