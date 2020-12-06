using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Model_Certainty
{
    float rate = 0;

    
    float oldExtraversion = 0;
    float oldNeuroticism = 0;

    int Ndirection = 0;//changes between two values (-1 and 1) depending on the direction of the change.
    int Edirection = 0;//changes between two values (-1 and 1) depending on the direction of the change.
    //static float THRESHOLD = 2.5f;

    int timepassed = 0;//time after last change to the value.

    public float getRate(float newNeuroticism, float newExtraversion)
    {
        CalcRate(newNeuroticism,newExtraversion);
        return rate;
    }
    void CalcRate(float newN, float newE)
    {
        int newNdirect = CheckDirection(newN, oldNeuroticism, Ndirection);
        int newEdirect = CheckDirection(newE, oldExtraversion, Edirection);
        GameObject.Find("Old_Number").GetComponent<Text>().text = timepassed.ToString();
        float neuroRate = newN - oldNeuroticism;
        float extraRate = newE - oldExtraversion;
        oldNeuroticism = newN;
        oldExtraversion = newE;
        
        ThresholdChecker(newNdirect, Ndirection, Math.Abs(neuroRate), 2.5f);
        ThresholdChecker(newEdirect, Edirection, Math.Abs(extraRate), 1);

        if (newNdirect != 0)
        {
            Ndirection = newNdirect;
        }
        if (newEdirect != 0)
        {
            Edirection = newEdirect;
        }
        /*
        if (timepassed/2 > 10)
        {
            //normalizes rate closer to zero if no changes have been made for 2 seconds.
            if (rate != 0)
            {
                if (rate > 0)
                {
                    rate = Math.Max(rate - 2,0);
                }
                else
                {
                    rate = Math.Min(rate + 2, 0);
                }
            }
            timepassed = 0;
        }
        */
    }

    void ThresholdChecker(int newDirection, int oldDirection, float value, float threshold)
    {
        if (newDirection != oldDirection)
        {
            if (value > threshold)
            {
                rate += value;
                timepassed = 0;
                return;
            }
        }else if(newDirection!=0 && newDirection == oldDirection)
        {
            if (value > threshold)
            {
                rate -= value;
                timepassed = 0;
                return;
            }                
        }
        timepassed += 1;
    }

    /// <summary>
    /// direction -1: newPos is less than old, direction 1: newPos is greater than old, direction 0: both equal.
    /// </summary>
    /// <param name="newPos"></param>
    /// <param name="oldPos"></param>
    /// <param name="direction"></param>
    int CheckDirection(float newPos, float oldPos, float direction)
    {

        if (newPos > oldPos)
        {
            return 1;
        }
        else if (newPos < oldPos)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
