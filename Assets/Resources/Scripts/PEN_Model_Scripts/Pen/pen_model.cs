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
    protected float certainty = 0;
    protected (string,string,int) characteristic;
    static float INTERVAL = 1;
    float timePassed = 0;

    public static float maxMovePart = 7;

    //for Neuroticism from movements.
    Move_neuroticism mover = new Move_neuroticism();

    //for Neuroticism from hastily attacks.
    Attack_neuroticism atk = new Attack_neuroticism();

    //for Extraversion from approach to enemies.
    Distance_extraversion dister = new Distance_extraversion();

    //for Rate of System Certainty
    Model_Certainty rate = new Model_Certainty();

    //for Characteristics
    Characteristics characts = new Characteristics();

    public float GetNeurotism()
    {
        return neuro;
    }
    public float GetExtraversion()
    {
        return extr;
    }
    public float GetCertainty()
    {
        return certainty;
    }

    public void SetMaxPen()
    {
        float maxE = GetExtraversion() + 10;
        float maxN_move = mover.getNeuro() + maxMovePart;
        float maxN_atk = atk.GetNeuro() + 10-maxMovePart;
        dister.SetMax(maxE);
        mover.SetMax(maxN_move);
        atk.SetMax(maxN_atk);
    }

    public void ResetBattle()
    {
        dister.ResetBattle();
        mover.ResetBattle();
    }

    /// <summary>
    /// Update works like the MonoBehavior Update and should be called like that. Calls every check for PEN alterations.
    /// </summary>
    /// <param name="dist"></param>
    /// <param name="enemyPos"></param>
    public void UpdateValues(GameObject player, GameObject enemy)
    {
        neuro = mover.getNeuro()+atk.GetNeuro();
        extr = dister.getExtr(player, enemy);
        timePassed += Time.deltaTime;
        if (timePassed >= INTERVAL)
        {
            timePassed = 0;            
            characteristic = characts.ExtractCharact(certainty, extr, neuro);
        }
        if (neuro_num != null && extra_num != null & psycho_num != null && character_text != null)
        {
            UpdateUI();
        }
    }

    public void UpdateRate()
    {
        certainty = rate.getRate(neuro, extr);
    }

    public void AttackNeuro(bool success)
    {
        atk.Attacked(success);
    }

    //Debug, visualized valued. Will be removed from the product.
    Text neuro_num = GameObject.Find("Neuro_Number").GetComponent<Text>();
    Text extra_num = GameObject.Find("Extra_Number").GetComponent<Text>();
    Text psycho_num = GameObject.Find("Psycho_Number").GetComponent<Text>();//psycho changed to certainty.
    Text character_text = GameObject.Find("CharacteristicText").GetComponent<Text>();

    void UpdateUI()
    {
        neuro_num.text = neuro.ToString();
        extra_num.text = extr.ToString();
        psycho_num.text = certainty.ToString();
        character_text.text = characteristic.Item1;
        if (characteristic.Item2 != null)
        {
            character_text.text += " (" + characteristic.Item2 + ")";
        }        
        character_text.text += ", " + characteristic.Item3.ToString();
    }
}
