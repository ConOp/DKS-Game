using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_extraversion
{
    private static float threshold = 5;
    private float extr = 0;
    private float oldpos = threshold;

    /// <summary>
    /// CheckDistance adds/ substracts from extraversion with distance in relation to the threshold.
    /// </summary>
    /// <param name="dist"></param>
    void CheckDistance(float dist)
    {
        if (dist <= threshold)
        {
            if (dist < oldpos)
            {
                extr += 0.001f;
            }
            else
            {
                extr -= 0.01f;
            }
            oldpos = dist;
        }        
    }

    /// <summary>
    /// getExtr calls CheckDistance and returns the exrtaversion value calculated.
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    public float getExtr(float dist)
    {
        CheckDistance(dist);
        return extr;
    }
}
