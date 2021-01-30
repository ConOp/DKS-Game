using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_neuroticism
{
    private float neuro = 0;
    private float maxNeuro;

    public float GetNeuro()
    {
        return neuro;
    }

    public void SetMax(float max)
    {
        maxNeuro = max;
    }

    float Normalizer()
    {
        float dist = maxNeuro - Math.Abs(neuro);
        dist = Math.Abs(dist / 10 + 0.01f);
        return dist;
    }

    public void Attacked(bool success)
    {
        if (Math.Abs(neuro) <= maxNeuro)
        {
            float multiplier = Normalizer();
            if (success)
            {
                neuro -= 0.5f * multiplier;
            }
            else
            {
                neuro += 0.5f * multiplier;
            }
        }
        else
        {
            neuro = neuro < 0 ? maxNeuro - maxNeuro : maxNeuro;
        }        
    }
}
