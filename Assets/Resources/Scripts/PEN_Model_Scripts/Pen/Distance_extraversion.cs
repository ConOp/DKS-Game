using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_extraversion
{
    private static float THRESHOLD = 15;
    private float extr = 0;
    private float oldpos = THRESHOLD;

    //player affected by this instance
    GameObject player;
    GameObject enemy;

    public Joystick move_joystick = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().joystick;
 
    /// <summary>
    /// CheckDistance adds/ substracts from extraversion with distance in relation to the threshold.
    /// </summary>
    /// <param name="dist"></param>
    void CheckDistance()
    {
        float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
        float angle = 60;
        if ((Math.Abs(move_joystick.Horizontal)>0.2f || Math.Abs(move_joystick.Vertical) > 0.2f) && dist <= THRESHOLD)
        {
            //if player is moving towards enemy
            if (Vector3.Angle(player.transform.forward, enemy.transform.position - player.transform.position) < angle)
            {
                extr += Math.Abs(oldpos - dist)/2;
            }else if(Vector3.Angle(-player.transform.forward, enemy.transform.position - player.transform.position) < angle)
            {
                //If player is moving away from enemy
                extr -= Math.Abs(oldpos - dist)/2;
            }
        }
        oldpos = dist;
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
