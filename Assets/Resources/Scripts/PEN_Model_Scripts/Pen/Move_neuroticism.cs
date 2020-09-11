using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_neuroticism
{

    private float time_w = 0;
    private float time_a = 0;
    private float time_s = 0;
    private float time_d = 0;
    public float threshold = 0.5f;
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
        if (joystick.Horizontal>0.2f)
        {
            time_w += Time.deltaTime;
        }
        if (joystick.Vertical < -0.2f)
        {
            time_a += Time.deltaTime;
        }
        if (joystick.Horizontal < -0.2f)
        {
            time_s += Time.deltaTime;
        }
        if (joystick.Vertical > 0.2f)
        {
            time_d += Time.deltaTime;
        }

        //when key is released, call function to calc time in regard to threshold.
        if (joystick.Horizontal<0.2f&&joystick.Horizontal>0)
        {
            MoveNeuro(ref time_w);
        }
        else if (joystick.Vertical > -0.2f && joystick.Vertical < 0)
        {
            MoveNeuro(ref time_a);
        }
        else if (joystick.Horizontal > -0.2f && joystick.Horizontal < 0)
        {
            MoveNeuro(ref time_s);
        }
        else if (joystick.Vertical < 0.2f && joystick.Vertical > 0)
        {
            MoveNeuro(ref time_d);
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
            neuro += (threshold - timer) / divider;
        }
        timer = 0;
        Debug.Log("neuro= " + neuro.ToString());
    }
}
