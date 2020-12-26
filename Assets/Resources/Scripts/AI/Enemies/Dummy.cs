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
            if (activeAbility.abilityname.Equals("Bash_Attack") && collision.transform.gameObject.tag.Equals("Wall"))
            {
                stunned = true;
                activeAbility.EndAttack();
                Invoke("Unstun", 3f);
            }
        }
    }

    protected override void MovementBehaviour()
    {
        
    }

}
