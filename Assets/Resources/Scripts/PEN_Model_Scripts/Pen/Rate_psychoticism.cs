using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rate_psychoticism
{
    float rate = 0;

    
    float oldExtraversion = 0;
    float oldNeuroticism = 0;
    static float THRESHOLD = 3;
    //x0-x1 where x changes each interval

    public float getPsycho(float newNeuroticism, float newExtraversion)
    {
        CalcRate(newNeuroticism,newExtraversion);
        return rate;
    }
    void CalcRate(float newN, float newE)
    {
        float neuroRate = newN - oldNeuroticism;
        float extraRate = newE - oldExtraversion;
        oldNeuroticism = newN;
        oldExtraversion = newE;
        ThresholdChecker(Math.Abs(neuroRate));
        ThresholdChecker(Math.Abs(extraRate));
    }

    void ThresholdChecker(float value)
    {
        if(value> THRESHOLD)
        {
            rate += value/10;
        }
        else
        {
            rate -= value/10;
        }
    }

}
