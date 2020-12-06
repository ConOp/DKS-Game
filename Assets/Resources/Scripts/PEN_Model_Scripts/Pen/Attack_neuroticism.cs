using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_neuroticism
{
    float neuro = 0;
    public float GetNeuro()
    {
        return neuro;
    }

    public void Attacked(bool success)
    {
        if (success)
        {
            neuro -= 0.5f;
        }
        else
        {
            neuro += 0.5f;
        }
    }
}
