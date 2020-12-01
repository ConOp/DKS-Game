using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour, RangedWeapon
{
    protected string weaponType = "handgun";
    [SerializeField]
    protected float damage = 10;
    [SerializeField]
    [Range(0.2f,1.5f)]
    protected float cooldown = 0.6f;
    [SerializeField]
    protected float range = 50;
    private float oldFire = 0;

    public string GetName()
    {
        return weaponType;
    }

    public float GetRange()
    {
        return range;
    }
    /// <summary>
    /// checks if the required time (cooldown) has passed from the last attack.
    /// </summary>
    /// <returns></returns>
    public bool ReadyToFire()
    {
        float time = Time.time;
        if (time <= oldFire + cooldown)
        {
            oldFire = time;
            return true;
        }

        return false;
    }
}
