using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pen_model
{
    //basic 3 axises for the Pen model
    //Neuroticism from 0 to 10
    //Extraversion from -10 to 10
    //Psychoticism from -10 to 10

    protected float neuro = 0;
    protected float extr = 0;
    protected float psycho = 0;

    //for Neuroticism
    Move_neuroticism mover = new Move_neuroticism();

    //for Extraversion
    Distance_extraversion dister = new Distance_extraversion();

    /// <summary>
    /// Update works like the MonBehavior Update and should be called like that. Calls every check for PEN alterations.
    /// </summary>
    /// <param name="dist"></param>
    /// <param name="enemyPos"></param>
    public void Update(float dist)
    {
        neuro = mover.getNeuro();
        extr = dister.getExtr(dist);
        GameObject.Find("Neuro_Number").GetComponent<Text>().text = neuro.ToString();
        GameObject.Find("Extra_Number").GetComponent<Text>().text = extr.ToString();
        //Debug.Log("Neuro: " + neuro + "\nExtra: " + extr);
    }

    public void LateUpdate()
    {

    }
}
