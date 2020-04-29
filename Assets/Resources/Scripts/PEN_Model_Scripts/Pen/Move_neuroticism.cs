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
        if (Input.GetKey(KeyCode.W))
        {
            time_w += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            time_a += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            time_s += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            time_d += Time.deltaTime;
        }

        //when key is released, call function to calc time in regard to threshold.
        if (Input.GetKeyUp(KeyCode.W))
        {
            MoveNeuro(ref time_w, KeyCode.W);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            MoveNeuro(ref time_a, KeyCode.A);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            MoveNeuro(ref time_s, KeyCode.S);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            MoveNeuro(ref time_d, KeyCode.D);
        }
    }

    /// <summary>
    /// adds / subtracts to neuroticism if button held for less / more time than the threshold.
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="key"></param>
    private void MoveNeuro(ref float timer, KeyCode key)
    {
        if (timer >= threshold)
        {
            neuro -= (timer - threshold) / divider;
        }
        else
        {
            neuro += (threshold - timer) / divider/2;
        }
        timer = 0;
        Debug.Log("neuro= " + neuro.ToString());
    }
}
