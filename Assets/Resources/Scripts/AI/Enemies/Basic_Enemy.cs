using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Basic_Enemy
{
    float health { get; set; }
    float movement_speed { get; set; }
    float damage { get; set; }
    List<(Vector3,string)> Attachments { get; set; }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public abstract void InitializeAttachments();

}
