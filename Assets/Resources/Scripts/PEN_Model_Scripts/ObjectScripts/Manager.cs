using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    bool inCombat = false;
    pen_model pen;

    public GameObject player;

    [HideInInspector]
    public List<GameObject> players;
    public List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        pen = new pen_model();

        player = GameObject.FindGameObjectWithTag("Player");

        
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
            foreach (GameObject enemy in enemies)
            {
                float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
                pen.Update(dist);
                enemy.GetComponent<EnemyMovement>().Move();
            }
            

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
