using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RandomProbability
{
    /// <summary>
    /// Random choice given some probabilities.
    /// </summary>
    /// <param name="probabilities"></param>
    /// <returns></returns>
 public static string Choose(List<(string,float)> probabilities  )
    {
        probabilities.Sort((p, q) => p.Item2.CompareTo(q.Item2));
        float maxprob = probabilities.Sum(s => s.Item2);
        double prob =UnityEngine.Random.Range(0,maxprob);
        int item=-1;
        double counter =0;
        while (prob > counter)
        {
            item++;
            counter += probabilities[item].Item2;
            
        }
        return probabilities[item].Item1;


    }
}
