using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public pen_model pen;
    float maxNeuro = 10;
    float maxExtra = 10;

    bool entering = true;
    bool unlocked = true;
    // Start is called before the first frame update
    void Start()
    {
        pen = new pen_model();
        pen.SetMaxNeuro(maxNeuro);
        pen.SetMaxExtra(maxExtra);
        entering = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<Player>().InCombat())
        {
            pen.UpdateRate();
            maxNeuro = pen.GetNeurotism() + 10;
            maxExtra = pen.GetExtraversion() + 10;
        }
        else
        {
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
            yield return new WaitForSeconds(0.5f);
            entering = false;
        }
    }
}
