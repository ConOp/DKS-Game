using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    Player boi;
    pen_model pen;
    public float dist;
    public Vector3 enemyPos;


    // Start is called before the first frame update
    void Awake()
    {
        boi = new Player(10);
        pen = new pen_model();
    }

    // Update is called once per frame
    void Update()
    {
        if (boi.InCombat())
        {
            pen.Update(dist);
        }
    }

    //enables Pen messurements
    public void startCombat()
    {
        boi.enterCombat();
        Debug.Log("Combat has started");
    }
    /*
    //addenemyPos to list
    public void addenemyPos(Vector3 item)
    {
        enemyPos.Add(item);
    }
    */
}
