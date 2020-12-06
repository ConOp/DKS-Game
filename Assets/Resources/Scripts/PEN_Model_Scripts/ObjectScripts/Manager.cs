using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    bool inCombat = false;
    

    GameObject player;

    [HideInInspector]
    public List<GameObject> players;
    [HideInInspector]
    public List<GameObject> enemies;
    GameObject distance_num;
    [HideInInspector]
    public float dist;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        try
        {
            distance_num = GameObject.Find("Distance_Number");
        }
        catch (Exception e) { };
        
        //unused for now...
        players = new List<GameObject>();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(p);
        }
        enemies = new List<GameObject>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (inCombat)
        {
            if (enemies.Any())
            {
                foreach(GameObject enemy in enemies)
                {
                    if (enemy.GetComponent<Enemy>().InCombat())
                    {
                        enemy.GetComponent<EnemyMovement>().Move();
                    }
                    else
                    {
                        enemies.Remove(enemy);
                    }
                }
            }
            else
            {
                player.GetComponent<Player>().exitCombat();
                inCombat = false;
                return;
            }           
            /*
            //distance of closest target to player.
            GameObject closestEnemy = Closest(player.transform.position, enemies);
            dist = Vector3.Distance(player.transform.position, closestEnemy.transform.position);
            distance_num.GetComponent<Text>().text = dist.ToString();
            */
        }
    }

    //enables Pen messurements
    public void startCombat(Collider obj)
    {
        obj.gameObject.GetComponent<Player>().enterCombat();
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().enterCombat();
        }
        inCombat = true;
        Debug.Log("Combat has started");
    }        
}
