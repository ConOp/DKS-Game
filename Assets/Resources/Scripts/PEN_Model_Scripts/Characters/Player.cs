using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,Character//MonoBehaviour, Character
{
    public int hp = 10;
    private bool combatant = false;
    /// <summary>
    /// creates a controlled player with given hp.
    /// </summary>
    public Player(int hp)
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
