using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_neuroticism
{

    private float moveTime_h = 0;
    private float moveTime_v = 0;
    public static float THRESHOLD = 1;
    private float neuro = 0;
    private float maxNeuro;

    public Joystick move_joystick = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().joystick;

    //to get neuro.
    public float getNeuro()
    {
        CheckMove();
        return neuro;
    }

    public void SetMax(float max)
    {
        maxNeuro = max;
    }

    float Normalizer()
    {
        float dist = Math.Abs(maxNeuro - 7) - Math.Abs(neuro);
        float rad = 1 + 0.5f * dist / 10;
        float multiplier = 1 - (float)Math.Abs(Math.Sin(Math.PI * rad));
        return multiplier;
    }

    public void ResetBattle()
    {
        moveTime_h = 0;
        moveTime_v = 0;
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

    /// <summary>
    /// adds / subtracts to neuroticism if move button held for less / more time than the threshold.
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="key"></param>
    private void ChangeNeuro(ref float timer)
    {
        if (neuro <= maxNeuro && neuro >= maxNeuro-pen_model.maxMovePart*2)
        {
            float multiplier = Normalizer();
            if (timer >= THRESHOLD)
            {
                neuro -= (timer - THRESHOLD) / 2 * multiplier;
            }
            else if (timer == 0)
            {
                return;
            }
            else
            {
                neuro += (THRESHOLD - timer) / 2 * multiplier;
            }
        }
        else
        {
            neuro = neuro<0?maxNeuro-pen_model.maxMovePart*2:maxNeuro;
        }
        timer = 0;

    }
}
