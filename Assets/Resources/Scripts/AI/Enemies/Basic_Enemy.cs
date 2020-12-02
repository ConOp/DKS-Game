using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic_Enemy:MonoBehaviour
{

    public float MOVEMENT_SPEED;
    public float ATTACK_SPEED;
    public float HEALTH;
    public float DAMAGE;
    public float RANGE;

    public float current_speed;
    public float current_attack_speed;
    public float current_health;
    public float current_range;
   
    public float max_movement_speed;
    public float current_damage;

    public List<(Transform base_position, string base_size,string base_side, bool used)> Attachment_Bases;
    public GameObject[] Attachments;

    public abstract void InitializeModificationBases();

    public void Appy_Modification_Boosts()
    {
        float totalmovementspeedboost = 0;
        float totalattackdamageboost = 0;
        float totalattackspeedboost = 0;
        float totalattackrangeboost = 0;
        for (int i = 0; i < Attachments.Length; i++)
        {
            if (Attachments[i] != null)
            {
                totalmovementspeedboost += Attachments[i].GetComponent<Modification>().movement_speed;
                totalattackdamageboost += Attachments[i].GetComponent<Modification>().attack_damage;
                totalattackspeedboost += Attachments[i].GetComponent<Modification>().attack_speed;
                totalattackrangeboost += Attachments[i].GetComponent<Modification>().attack_range;
            }
        }
        if (totalmovementspeedboost > 0)
        {
            max_movement_speed = MOVEMENT_SPEED * totalmovementspeedboost;
        }
        else
        {
            max_movement_speed = MOVEMENT_SPEED;
        }
        if (totalattackdamageboost > 0)
        {
            current_damage = DAMAGE * totalattackdamageboost;
        }
        else
        {
            current_damage = DAMAGE;
        }
        if (totalattackspeedboost > 0)
        {
            current_attack_speed = ATTACK_SPEED * totalattackspeedboost;
        }
        else
        {
            current_attack_speed = ATTACK_SPEED;
        }
        if (totalattackrangeboost > 0)
        {
            current_range = RANGE * totalattackrangeboost;
        }
        else
        {
            current_range = RANGE;
        }
    }

    /// <summary>
    /// Adds att
    /// </summary>
    /// <param name="mod_type"></param>
    public void Add_Modification(string mod_type)
    {
        Modification mod = Modification_Prefab_Manager.SearchModification(mod_type).GetComponent<Modification>();
        for (int i = 0; i < Attachment_Bases.Count; i++)
        {
            if (Attachment_Bases[i].base_size == mod.size && !Attachment_Bases[i].used)
            {
                GameObject modification = Rotate_Modification(mod, Attachment_Bases[i]);
                Attachments[i] = modification;
                Attachment_Bases[i] = (Attachment_Bases[i].base_position, Attachment_Bases[i].base_size,Attachment_Bases[i].base_side, true);
                Appy_Modification_Boosts();
                return;
            }
        }
        return;
    }
    /// <summary>
    /// Removes the selected modification and the modification boosts.
    /// </summary>
    /// <param name="mod_to_remove"></param>
    public void Remove_Modification(GameObject mod_to_remove)
    {
        int index = -1;
        for (int i = 0; i < Attachments.Length; i++)
        {
            if (Attachments[i] == mod_to_remove)
            {
                index = i;
            }
        }
        if (index == -1)
        {
            return;
        }
        else
        {
            Attachments[index].transform.parent = null;
            Attachments[index].GetComponent<Rigidbody>().isKinematic = false;
            Attachments[index].GetComponent<Collider>().isTrigger = false;
            Attachments[index].gameObject.GetComponent<Rigidbody>().AddExplosionForce(5f, Attachments[index].transform.position, 3f, 0f, ForceMode.Impulse);
            Attachments[index] = null;
            Attachment_Bases[index] = (Attachment_Bases[index].base_position, Attachment_Bases[index].base_size,Attachment_Bases[index].base_side, false);
            Appy_Modification_Boosts();
        }
    }

    /// <summary>
    /// Rotates the modification to be placed appropriately to fit on the enemy.
    /// </summary>
    /// <param name="mod"></param>
    /// <param name="mod_base"></param>
    /// <returns></returns>
    public GameObject Rotate_Modification(Modification mod,(Transform base_pos,string base_size,string base_side,bool taken) mod_base)
    {
        
        GameObject modificationobj = Instantiate(Modification_Prefab_Manager.SearchModification(mod.type), mod_base.base_pos.transform.position, new Quaternion(), gameObject.transform);

        if (mod_base.base_side == "E")
        {
           modificationobj.transform.Rotate(0, 180, 0);        }
        else if (mod_base.base_side == "N")
        {
            modificationobj.transform.Rotate(0, 0, -90);
        }
        else if (mod_base.base_side == "S")
        {
            modificationobj.transform.Rotate(0, 0, 90);
        }

        return modificationobj;
    }

    /// <summary>
    /// Receives damage when damaged by player.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool TakeDamage(float damage)
    {
        Debug.Log("taking damage " + damage);
        current_health -= damage;
        if (current_health <= 0)
        {
            Battle_Manager.GetInstance().RemoveEnemy(this.gameObject);//Remove enemy from battle.
            return true;
        }
        else
        {
            return false;
        }
    }

}
