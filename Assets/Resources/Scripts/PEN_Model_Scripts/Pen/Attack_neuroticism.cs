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
        float dist = Math.Abs(maxNeuro - 3) - Math.Abs(neuro);
        float rad = 1 + 0.5f * dist / 10;
        float multiplier = 1 - (float)Math.Abs(Math.Sin(Math.PI * rad));
        return multiplier;
    }

    public void Attacked(bool success)
    {
        if (neuro <= maxNeuro && neuro>=maxNeuro-(10-pen_model.maxMovePart)*2)
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
            neuro = neuro < 0 ? maxNeuro - (10-pen_model.maxMovePart)*2 : maxNeuro;
        }        
    }
}
