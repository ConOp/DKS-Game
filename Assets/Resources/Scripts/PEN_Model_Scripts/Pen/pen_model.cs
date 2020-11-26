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
    protected (string,string,int) characteristic;
    static float INTERVAL = 1;
    float timePassed = 0;

    Text neuro_num = GameObject.Find("Neuro_Number").GetComponent<Text>();
    Text extra_num = GameObject.Find("Extra_Number").GetComponent<Text>();
    Text psycho_num = GameObject.Find("Psycho_Number").GetComponent<Text>();
    Text character_text = GameObject.Find("CharacteristicText").GetComponent<Text>();

    //for Neuroticism
    Move_neuroticism mover = new Move_neuroticism();

    //for Extraversion
    Distance_extraversion dister = new Distance_extraversion();

    //for Psychoticism
    Rate_psychoticism rate = new Rate_psychoticism();

    //for Characteristics
    Characteristics characts = new Characteristics();
    
    /// <summary>
    /// Update works like the MonoBehavior Update and should be called like that. Calls every check for PEN alterations.
    /// </summary>
    /// <param name="dist"></param>
    /// <param name="enemyPos"></param>
    public void UpdateValues(GameObject player, GameObject enemy)
    {
        neuro = mover.getNeuro();
        extr = dister.getExtr(player, enemy);
        timePassed += Time.deltaTime;
        if (timePassed >= INTERVAL)
        {
            timePassed = 0;            
            characteristic = characts.ExtractCharact(psycho, extr, neuro);
        }
        if (neuro_num != null && extra_num != null & psycho_num != null && character_text != null)
        {
            UpdateUI();
        }
    }

    public void UpdatePsycho()
    {
        psycho = rate.getPsycho(neuro, extr);
    }

    void UpdateUI()
    {
        neuro_num.text = neuro.ToString();
        extra_num.text = extr.ToString();
        psycho_num.text = psycho.ToString();
        character_text.text = characteristic.Item1;
        if (characteristic.Item2 != null)
        {
            character_text.text += " (" + characteristic.Item2 + ")";
        }        
        character_text.text += ", " + characteristic.Item3.ToString();
    }
}
