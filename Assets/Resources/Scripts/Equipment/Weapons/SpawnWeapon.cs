using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeapon
{
    public static void Spawn(string weapon, Vector3 location, float id)
    {
        List<GameObject> weaponsList = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Equipment/Weapons/Melee"));
        bool isRanged = true;
        foreach (GameObject p in weaponsList)
        {
            if (p.name.Equals(weapon))
            {
                isRanged = false;
                break;
            }
        }
        if (isRanged)
        {
            weaponsList = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Equipment/Weapons/Ranged"));
        }
        foreach (GameObject prefab in weaponsList)
        {
            if (prefab.name.Equals(weapon))
            {
                GameObject g = GameObject.Instantiate(prefab, location, new Quaternion());
                g.GetComponent<WeaponData>().SetID(id);
                break;
            }
        }
    }
}
