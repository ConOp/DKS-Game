using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// creates a character with hp and attribute for if they are in combat.
/// </summary>
public interface Character
{


    /// <summary>
    /// Sets combatant value to true.
    /// </summary>
    void enterCombat();

    /// <summary>
    /// Sets combatant value to false.
    /// </summary>
    void exitCombat();

    /// <summary>
    /// Returns the value of combatant.
    /// </summary>
    /// <returns></returns>
    bool InCombat();
}
