using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Speed_Modification_1 : IModification
{
    public string type { get; set; }
    public float movement_speed_boost { get; set; }
    public float damage_boost { get; set; }
    public float health_boost { get; set; }
    public float attack_speed_boost { get; set; }
    public float mod_health { get; set; }
    public string size { get; set; }
    public Speed_Modification_1()
    {
        type= "Speed_Modification_1";
        movement_speed_boost = 1.1f;
        damage_boost = 1;
        health_boost = 1;
        attack_speed_boost = 1;
        mod_health = 50;
        size = "Medium";
    }
    /// <summary>
    /// Damages the modification and returns true if broken.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool TakeDamage(float damage)
    {
        mod_health -= damage;
        if (mod_health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
