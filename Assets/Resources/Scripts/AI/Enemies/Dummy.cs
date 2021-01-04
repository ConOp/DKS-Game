using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy:Basic_Enemy
{
    public  override void InitializeModificationBases()
    {
        Attachment_Bases = new List<(Transform, string,string,bool)>();
        foreach (Transform child in transform)
        {
            if (child.name == "Attachment1")
            {
                Attachment_Bases.Add((child, "Medium","E",false));
            }
            else if (child.name == "Attachment2")
            {
                Attachment_Bases.Add((child, "Medium","W",false));
            }
            else if (child.name == "Attachment3")
            {
                Attachment_Bases.Add((child, "Large","N",false));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (activeAbility != null)
        {
            if (collision.transform.gameObject.tag.Equals("Player"))
            {
                collision.transform.gameObject.GetComponent<Player>().TakeDamage(activeAbility.DealDamage());
            }
            else if (collision.transform.gameObject.tag.Equals("Wall"))
            {
                if (activeAbility.abilityname.Equals("Bash_Attack"))
                {
                    Stun();
                    activeAbility.EndAttack();
                }
            }
        }
    }

    protected override void MovementBehaviour()
    {
        
    }

}
