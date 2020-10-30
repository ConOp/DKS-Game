using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModification
{
    string type { get; set; }
    float movement_speed_boost { get; set; }
    float damage_boost { get; set; }
    float health_boost { get; set; }
    float attack_speed_boost { get; set; }
    float mod_health { get; set; }
    string size { get; set; }
    bool TakeDamage(float damage);
    
}
