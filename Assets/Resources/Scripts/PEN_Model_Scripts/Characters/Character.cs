using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// creates a character with hp and attribute for if they are in combat.
/// </summary>
public class Character
{
    protected int hp;
    private bool combatant = false;

    /// <summary>
    /// Sets combatant value to true.
    /// </summary>
    public void enterCombat()
    {
        this.combatant = true;
    }

    /// <summary>
    /// Sets combatant value to false.
    /// </summary>
    public void exitCombat()
    {
        this.combatant = false;
    }

    /// <summary>
    /// Returns the value of combatant.
    /// </summary>
    /// <returns></returns>
    public bool InCombat()
    {
        return combatant;
    }
}
