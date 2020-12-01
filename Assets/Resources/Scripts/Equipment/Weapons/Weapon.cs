using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon
{
    string GetName();
    bool ReadyToFire();
}
