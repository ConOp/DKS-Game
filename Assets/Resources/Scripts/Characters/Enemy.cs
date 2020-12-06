using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,Character
{
    [SerializeField]
    [Range(5f,100f)]
    protected float hp = 20f;
    private bool combatant = false;
    public Enemy(int hp)
    {
        this.hp = hp;
    }
    public void enterCombat()
    {
        this.combatant = true;
    }
    public void exitCombat()
    {
        this.combatant = false;
    }
    public bool InCombat()
    {
        return combatant;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Kill(0.5f);
        }
    }

    public void Kill(float delay)
    {
        this.exitCombat();
        Manager manager = GameObject.Find("Manager").GetComponent<Manager>();
        int index = manager.enemies.FindIndex(en => en == this.gameObject);
        manager.enemies.RemoveAt(index);
        Destroy(gameObject, delay);
    }

    public GameObject Closest(List<GameObject> targets)
    {
        float distance = Vector3.Distance(gameObject.transform.position, targets[0].transform.position);
        GameObject closest = targets[0];
        foreach (GameObject t in targets)
        {
            float d = Vector3.Distance(gameObject.transform.position, t.transform.position);
            if (d < distance)
            {
                distance = d;
                closest = t;
            }
        }
        return closest;
    }
}
