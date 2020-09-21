using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pen_model
{
    //basic 3 axises for the Pen model

    protected float neuro = 0;
    protected float extr = 0;
    protected float psycho = 0;
    static float INTERVAL = 1;
    float timePassed = 0;

    GameObject neuro_num = GameObject.Find("Neuro_Number");
    GameObject extra_num = GameObject.Find("Extra_Number");
    GameObject psycho_num = GameObject.Find("Psycho_Number");

    //for Neuroticism
    Move_neuroticism mover = new Move_neuroticism();

    //for Extraversion
    Distance_extraversion dister = new Distance_extraversion();

    //for Psychoticism
    Rate_psychoticism rate = new Rate_psychoticism();

    /// <summary>
    /// Update works like the MonBehavior Update and should be called like that. Calls every check for PEN alterations.
    /// </summary>
    /// <param name="dist"></param>
    /// <param name="enemyPos"></param>
    public void Update(GameObject player, GameObject enemy)
    {
        neuro = mover.getNeuro();
        extr = dister.getExtr(player, enemy);
        timePassed += Time.deltaTime;
        if (timePassed >= INTERVAL)
        {
            timePassed = 0;
            psycho = rate.getPsycho(neuro, extr);
        }
        neuro_num.GetComponent<Text>().text = neuro.ToString();
        extra_num.GetComponent<Text>().text = extr.ToString();
        psycho_num.GetComponent<Text>().text = psycho.ToString();
    }
}
