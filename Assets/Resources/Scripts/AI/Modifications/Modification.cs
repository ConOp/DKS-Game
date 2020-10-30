using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modification : MonoBehaviour
{
    public float mod_health = 100;
    public string type;
    public string size;
    public float movement_speed;

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            if (TakeDamage(collision.gameObject.GetComponent<Bullet>().DAMAGE))
            {
                transform.parent.GetComponent<Dummy>().Remove_Attachment(gameObject);
                Destroy(collision.gameObject);
            }
            Debug.Log("HIT");
        }
    }

    public bool TakeDamage(float damage)
    {
        mod_health -= damage;
        if (mod_health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
