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
    private Dictionary<(int, int), (string, string)> characterDictionary = new Dictionary<(int, int), (string, string)>();

    public Characteristics()
    {
        characterDictionary.Add((-70, 30), ("Melancholic", "Quiet"));
        characterDictionary.Add((-50, 50), ("Melancholic", "Rigid"));
        characterDictionary.Add((-30, 70), ("Melancholic", "Anxious"));

        characterDictionary.Add((30, 70), ("Choleric", "Restless"));
        characterDictionary.Add((50, 50), ("Choleric", "Aggressive"));
        characterDictionary.Add((70, 30), ("Choleric", "Impulsive"));

        characterDictionary.Add((70, -30), ("Sanguine", "Sociable"));
        characterDictionary.Add((50, -50), ("Sanguine", "Relaxed"));
        characterDictionary.Add((30, -70), ("Sanguine", "Carefree"));

        characterDictionary.Add((-30, -70), ("Phlegmatic", "Calm"));
        characterDictionary.Add((-50, -50), ("Phlegmatic", "Controlled"));
        characterDictionary.Add((-70, -30), ("Phlegmatic", "Thoughtful"));

        characterDictionary.Add((0, 0), ("Neutral", null));
    }
    
    public (string,string) GetCharacteristic(int Extr, int Neuro)
    {
        if (characterDictionary.ContainsKey((Extr, Neuro)))
        {
            return characterDictionary[(Extr, Neuro)];
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
        (float, float) closerPos = (101, 101);
        foreach(var character in this.characterDictionary)
        {
            bool comp1 = Math.Abs(Extra) - Math.Abs(character.Key.Item1) < closerPos.Item1;
            bool comp2 = Math.Abs(Extra) - Math.Abs(character.Key.Item2) < closerPos.Item2;

            if (comp1 && comp2)
            {
                closerPos = (Math.Abs(character.Key.Item1), Math.Abs(character.Key.Item2));
                closerName = (character.Value.Item1, character.Value.Item2);
            }
        }
        string appearing = closerName.Item1 + " (" + closerName.Item2 + ")";
        int state = (int) -Psycho / 20;
        return (closerName.Item1,closerName.Item2,state);
    }
}
