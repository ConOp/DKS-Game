using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    float getAttackSpeed();
    string GetTypeOfWeapon();
    bool ReadyToAttack();
    bool Attack(GameObject target);
    float GetRange();
}
