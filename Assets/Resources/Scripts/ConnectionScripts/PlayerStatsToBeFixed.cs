using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsToBeFixed : MonoBehaviour
{
    //player's info
    public int id;              //local player's client id
    public string username;
    public float current_health;
    public float maximum_health;
    public MeshRenderer meshRenderer;

    public void Initialize(int player_id, string uname)
    {
        id = player_id;                                     //assign id, username to player gameobject
        username = uname;
        current_health = maximum_health;
    }

    public void setHealth(float health) 
    {
        current_health = health;
        if (current_health <= 0f) {
            Die();
        }
    }

    public void Die() {
        meshRenderer.enabled = false;                       //disable player model of the dead one
    }

    public void Regenerate() 
    {
        meshRenderer.enabled = true;
        setHealth(maximum_health);
    }
}
