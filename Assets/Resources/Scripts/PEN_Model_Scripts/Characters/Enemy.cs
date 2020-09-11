using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character//MonoBehaviour,Character
{
    protected int hp;
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
