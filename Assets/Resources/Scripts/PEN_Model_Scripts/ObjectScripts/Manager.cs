using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    bool inCombat = false;
    pen_model pen;

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
        pen = new pen_model();

        player = GameObject.FindGameObjectWithTag("Player");

        distance_num = GameObject.Find("Distance_Number");

        enemies = new List<GameObject>();
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(e);
        }

        //unused for now...
        players = new List<GameObject>();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(p);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inCombat)
        {
            if (enemies.Any())
            {
                enemies?.ForEach(enemy =>
                {
                    if (enemy.GetComponent<Enemy>().InCombat())
                    {
                        enemy.GetComponent<EnemyMovement>().Move();
                    }
                    else
                    {
                        enemies.Remove(enemy);
                    }
                });
            }
            else
            {
                player.GetComponent<Player>().exitCombat();
                inCombat = false;
                return;
            }           

            //distance of closest target to player.
            GameObject closest = Closest(player, enemies);
            dist = Vector3.Distance(player.transform.position, closest.transform.position);
            distance_num.GetComponent<Text>().text = dist.ToString();
            pen.Update(player,closest);
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

    /// <summary>
    /// Returns the closest object of a list of GameObjects to a GameObject source.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="targets"></param>
    /// <returns></returns>
    public GameObject Closest(GameObject source, List<GameObject> targets)
    {
        float distance = Vector3.Distance(source.transform.position, targets[0].transform.position);
        GameObject closest = targets[0];
        foreach (GameObject t in targets)
        {
            float d = Vector3.Distance(source.transform.position, t.transform.position);
            if (d < distance)
            {
                distance = d;
                closest = t;
            }
        }
        return closest;
    }
}
