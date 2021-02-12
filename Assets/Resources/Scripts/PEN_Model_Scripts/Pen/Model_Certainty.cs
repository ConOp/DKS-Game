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


    public float getRate(float newNeuroticism, float newExtraversion)
    {
        CalcRate(newNeuroticism,newExtraversion);
        return rate;
    }
    void CalcRate(float newN, float newE)
    {
        int newNdirect = CheckDirection(newN, oldNeuroticism, Ndirection);
        int newEdirect = CheckDirection(newE, oldExtraversion, Edirection);
        float neuroRate = Math.Abs(newN - oldNeuroticism);
        float extraRate = Math.Abs(newE - oldExtraversion);
        oldNeuroticism = newN;
        oldExtraversion = newE;
        
        ThresholdChecker(newNdirect, Ndirection, neuroRate, 1);
        ThresholdChecker(newEdirect, Edirection, extraRate, 1);

        if (newNdirect != 0)
        {
            Ndirection = newNdirect;
        }
        if (newEdirect != 0)
        {
            Edirection = newEdirect;
        }
    }

    void ThresholdChecker(int newDirection, int oldDirection, float value, float threshold)
    {
        if (newDirection != oldDirection)
        {
            if (value > threshold)
            {
                rate -= value;
                return;
            }
        }else if(newDirection!=0 && newDirection == oldDirection)
        {
            if (value > threshold)
            {
                rate += value;
                return;
            }                
        }
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
