using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_neuroticism
{

    private float moveTime_h = 0;
    private float moveTime_v = 0;
    private float hitTime_h = 0;
    private float hitTime_v = 0;
    public static float THRESHOLD = 1;
    private float neuro = 0;


    public Joystick move_joystick = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().joystick;
    public Joystick hit_joystick = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHit>().joystick;

    //to get neuro.
    public float getNeuro()
    {
        CheckMove();
        //CheckHit();//old hit controller
        return neuro;
    }
    /// <summary>
    ///CheckMove is called whenever someone wants to get neuro from movement.
    /// </summary>
    public void CheckMove()
    {
        //when character is moving, count time of holding joystick
        if (Math.Abs(move_joystick.Horizontal)>0.2f)
        {
            moveTime_h += Time.deltaTime;
        }
        if (Math.Abs(move_joystick.Vertical) > 0.2f)
        {
            moveTime_v += Time.deltaTime;
        }

        //when key is released, call function to calc time in regard to threshold.
        if (Math.Abs(move_joystick.Horizontal) < 0.2f && Math.Abs(move_joystick.Horizontal) > 0)
        {
            ChangeNeuro(ref moveTime_h);
        }
        else if (Math.Abs(move_joystick.Vertical) < 0.2f && Math.Abs(move_joystick.Vertical) > 0)
        {
            ChangeNeuro(ref moveTime_v);
        }
    }

    public void CheckHit()
    {
        //when key is hold, count time.
        if (Math.Abs(hit_joystick.Horizontal) > 0.2f)
        {
            hitTime_h += Time.deltaTime;
        }
        if (Math.Abs(hit_joystick.Vertical) > 0.2f)
        {
            hitTime_v += Time.deltaTime;
        }

        //when key is released, call function to calc time in regard to threshold.
        if (Math.Abs(hit_joystick.Horizontal) < 0.2f && Math.Abs(hit_joystick.Horizontal) > 0)
        {
            ChangeNeuro(ref hitTime_h);
        }
        else if (Math.Abs(hit_joystick.Vertical) < 0.2f && Math.Abs(hit_joystick.Vertical) > 0)
        {
            ChangeNeuro(ref hitTime_v);
        }
    }

    /// <summary>
    /// adds / subtracts to neuroticism if move button held for less / more time than the threshold.
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="key"></param>
    private void ChangeNeuro(ref float timer)
    {
        if (timer >= THRESHOLD)
        {
            neuro -= (timer - THRESHOLD) / 2;
        }
        else if (timer == 0)
        {
            return;
        }
        else
        {
            neuro += (THRESHOLD - timer)/2;
        }
        timer = 0;
    }
}
