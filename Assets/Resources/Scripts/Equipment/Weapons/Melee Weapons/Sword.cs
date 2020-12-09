using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, MeleeWeapon
{
    #region attributes
    protected string weaponType = "ranged";
    [SerializeField]
    [Range(5f, 50f)]
    protected float damage = 15f;
    [SerializeField]
    [Range(1f, 20f)]
    protected float range = 2f;
    [SerializeField]
    [Range(0.2f, 1.5f)]
    protected float cooldown = 0.6f;
    [SerializeField]
    [Range(1f, 360f)]
    protected float arc = 120f;
    private float oldFire = 0f;
    #endregion

    public string GetTypeOfWeapon()
    {
        return weaponType;
    }

    public float getAttackSpeed()
    {
        return cooldown;
    }

    public float getArc()
    {
        return arc;
    }

    public float GetRange()
    {
        return range;
    }

    public bool ReadyToAttack()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WeaponIdle"))
        {
            return true;
        }
        return false;        
    }

    public bool Attack()
    {
        if (ReadyToAttack())
        {
            GetComponent<Animator>().SetTrigger("Swing");
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) > range)
                {
                    continue;
                }
                //if enemy is withing the weapon arc
                if (Vector3.Angle(transform.parent.parent.forward, enemy.transform.position - transform.parent.parent.position) <= arc)
                {                    
                    enemy.GetComponent<Basic_Enemy>().TakeDamage(damage);
                }
            }
            return true;
        }
        return false;
    }

}
