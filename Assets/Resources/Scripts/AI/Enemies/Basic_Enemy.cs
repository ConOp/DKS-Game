using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic_Enemy:MonoBehaviour
{

    public float MOVEMENT_SPEED;
    public float HEALTH;
    public float DAMAGE;

    public float current_speed;
    public float current_health;
    public float max_movement_speed;
    public float current_damage;

    public List<(Transform base_position, string base_size, bool used)> Attachment_Bases;
    public GameObject[] Attachments;

    public abstract void InitializeModificationBases();

    public void Appy_Modification_Boosts()
    {
        float totalspeedboost = 0;
        for (int i = 0; i < Attachments.Length; i++)
        {
            if (Attachments[i] != null)
            {
                totalspeedboost += Attachments[i].GetComponent<Modification>().movement_speed;
            }
        }
        if (totalspeedboost > 0)
        {
            max_movement_speed = MOVEMENT_SPEED * totalspeedboost;
        }
        else
        {
            max_movement_speed = MOVEMENT_SPEED;
        }
    }

    public void Add_Attachment(string mod_type)
    {
        Modification mod = Modification_Prefab_Manager.SearchModification(mod_type).GetComponent<Modification>();
        for (int i = 0; i < Attachment_Bases.Count; i++)
        {
            if (Attachment_Bases[i].base_size == mod.size && !Attachment_Bases[i].used)
            {
                GameObject modification = Instantiate(Modification_Prefab_Manager.SearchModification(mod.type), Attachment_Bases[i].base_position.transform.position, new Quaternion(), gameObject.transform);
                Attachments[i] = modification;
                Attachment_Bases[i] = (Attachment_Bases[i].base_position, Attachment_Bases[i].base_size, true);
                Appy_Modification_Boosts();
                return;
            }
        }
        return;
    }

    public void Remove_Attachment(GameObject mod_to_remove)
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
            //Destroy(Attachments[index].GetComponent<FixedJoint>());
            Attachments[index].transform.parent = null;
            Attachments[index].GetComponent<Rigidbody>().isKinematic = false;
            Attachments[index].GetComponent<Collider>().isTrigger = false;
            Attachments[index].gameObject.GetComponent<Rigidbody>().AddExplosionForce(5f, Attachments[index].transform.position, 3f, 0f, ForceMode.Impulse);
            Attachments[index] = null;
            Attachment_Bases[index] = (Attachment_Bases[index].base_position, Attachment_Bases[index].base_size, false);
            Appy_Modification_Boosts();
        }
    }

    public bool TakeDamage(float damage)
    {
        Debug.Log("taking damage " + damage);
        current_health -= damage;
        if (current_health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
