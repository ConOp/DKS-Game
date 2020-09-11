using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_neuroticism
{

    private float time_h = 0;
    private float time_v = 0;
    public float threshold = 1;
    public int divider = 20;
    private float neuro = 0;

    public Joystick joystick = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().joystick;

    //to get neuro.
    public float getNeuro()
    {
        CheckMove();
        return neuro;
    }
    /// <summary>
    ///CheckMove is called whenever someone wants to get neuro from here.
    /// </summary>
    public void CheckMove()
    {
        //when key is hold, count time.
        if (Math.Abs(joystick.Horizontal)>0.2f)
        {
            time_h += Time.deltaTime;
        }
        if (Math.Abs(joystick.Vertical) > 0.2f)
        {
            time_v += Time.deltaTime;
        }

        //when key is released, call function to calc time in regard to threshold.
        if (Math.Abs(joystick.Horizontal) < 0.2f && Math.Abs(joystick.Horizontal) > 0)
        {
            MoveNeuro(ref time_h);
        }
        else if (Math.Abs(joystick.Vertical) < 0.2f && Math.Abs(joystick.Vertical) > 0)
        {
            MoveNeuro(ref time_v);
        }
    }

    /// <summary>
    /// adds / subtracts to neuroticism if button held for less / more time than the threshold.
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="key"></param>
    private void MoveNeuro(ref float timer)
    {
        if (timer >= threshold)
        {
            neuro -= (timer - threshold) / divider;
        }
        else if (timer == 0)
        {
            return;
        }
        else
        {
            neuro += (threshold - timer) / divider*2;
        }
        timer = 0;
        Debug.Log("neuro= " + neuro.ToString());
    }
}
