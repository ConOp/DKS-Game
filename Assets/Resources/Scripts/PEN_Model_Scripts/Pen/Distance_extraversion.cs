using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_extraversion
{
    private static float THRESHOLD = 105;
    private float extr = 0;
    private float oldpos = THRESHOLD;
    private float maxExtra;

    //player affected by this instance
    GameObject player;
    GameObject enemy;

    public Joystick move_joystick = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().joystick;
 
    public void SetMax(float max)
    {
        maxExtra = max;
    }

    float Normalizer()
    {
        float dist = maxExtra - Math.Abs(extr);
        dist = Math.Abs(dist/10+0.01f);
        return dist;
    }

    /// <summary>
    /// CheckDistance adds/ substracts from extraversion with distance in relation to the threshold.
    /// </summary>
    /// <param name="dist"></param>
    void CheckDistance()
    {
        if (extr <= maxExtra && extr >=maxExtra-20)
        {
            float multiplier = Normalizer();
            float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
            float angle = 60;
            bool seen = enemy.GetComponentInChildren<Renderer>().isVisible;
            if ((Math.Abs(move_joystick.Horizontal) > 0.2f || Math.Abs(move_joystick.Vertical) > 0.2f) && dist <= THRESHOLD && seen)
            {
                //if player is moving towards enemy
                if (Vector3.Angle(player.transform.forward, enemy.transform.position - player.transform.position) < angle)
                {
                    extr += Math.Abs(oldpos - dist) / 3 * multiplier;
                }
                else if (Vector3.Angle(-player.transform.forward, enemy.transform.position - player.transform.position) < angle)
                {
                    //If player is moving away from enemy
                    extr -= Math.Abs(oldpos - dist) / 3 * multiplier;
                }
            }
            oldpos = dist;
        }
        else
        {
            extr = extr < 0 ? -maxExtra : maxExtra;
        }
        
    }

    /// <summary>
    /// getExtr calls CheckDistance and returns the exrtaversion value calculated.
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    public float getExtr(GameObject player, GameObject enemy)
    {
        this.player = player;
        this.enemy = enemy;
        CheckDistance();
        return extr;
    }
}
