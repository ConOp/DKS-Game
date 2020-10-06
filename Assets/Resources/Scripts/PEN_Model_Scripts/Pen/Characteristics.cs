using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristics
{
    //Extraversion and Neuroticism values in the characteristics represent the location of the points.
    /// <summary>
    /// Characteristics are represented as key:(Extraversion, Neuroticism) and value: (Category, Name).
    /// </summary>
    private Dictionary<Vector2, (string, string)> characterDictionary = new Dictionary<Vector2, (string, string)>();

    public Characteristics()
    {
        characterDictionary.Add(new Vector2(-70, 30), ("Melancholic", "Quiet"));
        characterDictionary.Add(new Vector2(-50, 50), ("Melancholic", "Rigid"));
        characterDictionary.Add(new Vector2(-30, 70), ("Melancholic", "Anxious"));
        characterDictionary.Add(new Vector2(-70, 70), ("True Melancholic", null));

        characterDictionary.Add(new Vector2(30, 70), ("Choleric", "Restless"));
        characterDictionary.Add(new Vector2(50, 50), ("Choleric", "Aggressive"));
        characterDictionary.Add(new Vector2(70, 30), ("Choleric", "Impulsive"));
        characterDictionary.Add(new Vector2(70, 70), ("True Choleric", null));

        characterDictionary.Add(new Vector2(70, -30), ("Sanguine", "Sociable"));
        characterDictionary.Add(new Vector2(50, -50), ("Sanguine", "Relaxed"));
        characterDictionary.Add(new Vector2(30, -70), ("Sanguine", "Carefree"));
        characterDictionary.Add(new Vector2(70, -70), ("True Sanguine", null));

        characterDictionary.Add(new Vector2(-30, -70), ("Phlegmatic", "Calm"));
        characterDictionary.Add(new Vector2(-50, -50), ("Phlegmatic", "Controlled"));
        characterDictionary.Add(new Vector2(-70, -30), ("Phlegmatic", "Thoughtful"));
        characterDictionary.Add(new Vector2(-70, -70), ("True Phlegmatic", null));

        characterDictionary.Add(new Vector2(0, 0), ("Neutral", null));

        characterDictionary.Add(new Vector2(-30, 0), ("Introvert", null));
        characterDictionary.Add(new Vector2(-60, 0), ("True Introvert", null));

        characterDictionary.Add(new Vector2(30, 0), ("Extravert", null));
        characterDictionary.Add(new Vector2(60, 0), ("True Extravert", null));

        characterDictionary.Add(new Vector2(0, -30), ("Stable", null));
        characterDictionary.Add(new Vector2(0, -60), ("True Stability", null));

        characterDictionary.Add(new Vector2(0, 30), ("Neurotic", null));
        characterDictionary.Add(new Vector2(0, 60), ("True Neurotic", null));
    }
    
    public (string,string) GetCharacteristic(int Extr, int Neuro)
    {
        Vector2 vector = new Vector2(Extr, Neuro);
        if (characterDictionary.ContainsKey(vector))
        {
            return characterDictionary[vector];
        }
        return (null, null);
    }

    /// <summary>
    /// Returns a tuple of (Category, Name, Psychoticism state)
    /// </summary>
    /// <param name="Extra"></param>
    /// <param name="Neuro"></param>
    /// <param name="Psycho"></param>
    /// <returns></returns>
    public (string,string,int) ExtractCharact(float Psycho, float Extra, float Neuro)
    {
        (string,string) closerName = (null,null);
        float closerPos = 101;
        foreach(var character in this.characterDictionary)
        {
            //bool comp1 = Math.Abs(Extra) - Math.Abs(character.Key.x) < closerPos.Item1;
            //bool comp2 = Math.Abs(Extra) - Math.Abs(character.Key.y) < closerPos.Item2;

            //if (comp1 && comp2)
            Vector2 vector = new Vector2(Extra, Neuro);
            float distance = Vector2.Distance(vector, character.Key);
            if(distance<closerPos)
            {
                closerPos = distance;//(Math.Abs(character.Key.x), Math.Abs(character.Key.Item2));
                closerName = (character.Value.Item1, character.Value.Item2);
            }
        }
        string appearing = closerName.Item1 + " (" + closerName.Item2 + ")";
        int state = (int) -Psycho / 20;
        return (closerName.Item1,closerName.Item2,state);
    }
}
