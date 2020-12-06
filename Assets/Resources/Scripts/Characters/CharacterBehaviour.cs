using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public pen_model pen;
    Manager mng;
    // Start is called before the first frame update
    void Start()
    {
        pen = new pen_model();
        mng = GameObject.Find("Manager").GetComponent<Manager>();
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
            pen.UpdateValues(gameObject, GetComponent<Player>().Closest(mng.enemies));
        }
    }
}
