using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Basic_Enemy
{
    public override void InitializeModificationBases()
    {
        Attachment_Bases = new List<(Transform, string, string, bool)>();
        foreach (Transform child in transform)
        {
            if (child.name == "Attachment1")
            {
                Attachment_Bases.Add((child, "Medium", "E", false));
            }
            else if (child.name == "Attachment2")
            {
                Attachment_Bases.Add((child, "Medium", "W", false));
            }
        }
    }

    protected override void MovementBehaviour()
    {
        if (target != null)
        {
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) < current_range * 0.8)
            {
                transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, -1 * current_speed * Time.deltaTime);
            }
        }
    }
}
