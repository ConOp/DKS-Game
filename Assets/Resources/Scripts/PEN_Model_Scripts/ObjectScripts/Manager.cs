using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    Player boi;
    Enemy cube;
    pen_model pen;
    public Vector3 enemyPos;

    private GameObject player;
    private GameObject enemy;


    // Start is called before the first frame update
    void Awake()
    {
        boi = new Player(10);
        cube = new Enemy(5);
        pen = new pen_model();

        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (boi.InCombat())
        {
            float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
            pen.Update(dist);
            GameObject.Find("Enemy").GetComponent<EnemyMovement>().Move();
        }
    }

    //enables Pen messurements
    public void startCombat()
    {
        boi.enterCombat();
        cube.enterCombat();
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
