using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    float getAttackSpeed();
    string GetTypeOfWeapon();
    bool ReadyToAttack();
    void Attack();
    float GetRange();
}
