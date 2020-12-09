using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    bool lockOn = false;
    GameObject same = null;
    GameObject arrow;
    GameObject targeted;

    public GameObject arrows;
    public GameObject hand;

    // Update is called once per frame
    void Update()
    {

        Battle currentBattle = Battle_Manager.GetInstance().GetBattle(gameObject);
        if (currentBattle!=null)
        {
            if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began)
            {
                SelectEnemy(Input.GetTouch(Input.touchCount - 1),currentBattle.GetEnemies());
            }
            if (!lockOn)
            {
                try
                {
                    targeted = GetComponent<Player>().Closest(currentBattle.GetEnemies());
                    TargetLock(targeted);
                }catch(Exception e) { }
                
            }
            else
            {
                TargetLock(targeted);
            }
            if (targeted != null)
            {
                PointToEnemy();
            }
        }
        
        
    }

    void PointToEnemy()
    {
        transform.LookAt(targeted.transform);
    }

    void SelectEnemy(Touch finger,List<GameObject> enemies)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.position);
        if(Physics.Raycast(ray,out hit, Mathf.Infinity,LayerMask.GetMask("Ground")))
        {
            GameObject closest = GetComponent<Player>().Closest(enemies);
            float dist = Vector2.Distance(hit.point, closest.transform.position);
            if (dist < 0.9f)
            {
                lockOn = true;
                targeted = closest;
            }
        }
    }

    void TargetLock(GameObject closest)
    {
        if (closest != same)
        {
            Destroy(arrow);
            same = closest;
            arrow = Instantiate(arrows, new Vector3(closest.transform.position.x, 0, closest.transform.position.z), Quaternion.identity);
            arrow.transform.parent = closest.transform;
        }
    }

    public void UnLock()
    {
        lockOn = false;
    }
}
