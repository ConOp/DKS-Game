using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public pen_model pen;
    // Start is called before the first frame update
    void Start()
    {
        pen = new pen_model();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Player>().InCombat())
        {
            pen.UpdateRate();
        }
        else
        {
            List<GameObject> temp = Battle_Manager.GetInstance().GetBattle(gameObject).GetEnemies();
            if (temp.Any())
            {
                pen.UpdateValues(gameObject, GetComponent<Player>().Closest(temp));
            }
        }
    }
}
