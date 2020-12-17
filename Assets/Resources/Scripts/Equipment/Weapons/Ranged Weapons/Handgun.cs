using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour, RangedWeapon
{
    #region attributes
    protected string weaponType = "ranged";
    [SerializeField]
    [Range(5f,50f)]
    protected float damage = 10f;
    [SerializeField]
    [Range(5f,50f)]
    protected float speed = 20f;
    [SerializeField]
    [Range(0.2f,1.5f)]
    protected float cooldown = 0.6f;
    [SerializeField]
    [Range(1f,100f)]
    protected float range = 12f;
    private float oldFire = 0f;
    #endregion

    public GameObject bullet;

    public string GetTypeOfWeapon()
    {
        return weaponType;
    }

    public float GetRange()
    {
        return range;
    }

    public float getAttackSpeed()
    {
        return cooldown;
    }

    public float GetSpeed()
    {
        return speed;
    }
    /// <summary>
    /// checks if the required time (cooldown) has passed from the last attack.
    /// </summary>
    /// <returns></returns>
    public bool ReadyToAttack()
    {
        float time = Time.time;
        if (time >= oldFire + cooldown)
        {
            oldFire = time;
            return true;
        }

        return false;
    }

    public bool Attack(GameObject target)
    {
        if (ReadyToAttack())
        {
            GameObject bullet = Instantiate(this.bullet, gameObject.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet_v2>().target = target;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Bullet_v2>().range = range;
            bullet.GetComponent<Bullet_v2>().speed = speed;
            bullet.GetComponent<Bullet_v2>().damage = damage;
            return true;
        }
        return false;
    }
}
