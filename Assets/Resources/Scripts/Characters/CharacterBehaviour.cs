using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public pen_model pen;

    bool entering = true;
    bool unlocked = true;
    bool updater = false;
    // Start is called before the first frame update
    void Start()
    {
        pen = new pen_model();
        pen.SetMaxPen();
        entering = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Player>().InCombat())
        {
            if (updater)
            {
                updater = false;
                pen.UpdateRate();
                pen.SetMaxPen();
            }            
        }
        else
        {
            updater = true;
            if (entering)
            {
                StartCoroutine(Waiter());
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

    IEnumerator Waiter()
    {
        if (entering && unlocked)
        {
            unlocked = false;
            yield return new WaitForSeconds(1f);
            entering = false;
        }
    }
}
