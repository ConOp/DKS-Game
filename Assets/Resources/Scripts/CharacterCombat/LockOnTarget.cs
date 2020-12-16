using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LockOnTarget : MonoBehaviour
{
    bool lockOn = false;
    GameObject same = null;
    GameObject lockIcon;
    GameObject targetedCreature;
    GameObject targeted;
    GameObject[] attachments;

    public GameObject arrows;
    public GameObject body;

    //temp
    public GameObject target;

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
                    GameObject target = GetComponent<Player>().Closest(currentBattle.GetEnemies());
                    if (target != targetedCreature)
                    {
                        targetedCreature = target;
                        TargetLock(targetedCreature);
                    }
                }catch(Exception e) { }
                
            }
            else
            {
                if (targetedCreature)
                {
                    TargetLock(targetedCreature);
                }
            }

            if (targetedCreature != null)
            {
                PointToEnemy();
            }
        }
    }

    void PointToEnemy()
    {
        body.transform.LookAt(targetedCreature.transform);
    }

    void SelectEnemy(Touch finger,List<GameObject> enemies)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.position);
        if(Physics.Raycast(ray,out hit, Mathf.Infinity,LayerMask.GetMask("Ground")))
        {
            GameObject closest = Closest(hit.point,enemies);
            float dist = Vector2.Distance(hit.point, closest.transform.position);
            if (dist < 2f)
            {
                lockOn = true;
                targetedCreature = closest;
                targeted = targetedCreature;
            }
        }
    }
    public GameObject Closest(Vector3 hit, List<GameObject> targets)
    {
        float distance = Vector3.Distance(hit, targets[0].transform.position);
        GameObject closest = targets[0];
        foreach (GameObject t in targets)
        {
            float d = Vector3.Distance(hit, t.transform.position);
            if (d < distance)
            {
                distance = d;
                closest = t;
            }
        }
        return closest;
    }

    void TargetLock(GameObject closest)
    {
        if (closest != same)
        {
            Destroy(lockIcon);
            same = closest;
            lockIcon = Instantiate(target,new Vector3(0, 2, 2), Quaternion.identity);
            //lockIcon = Instantiate(arrows, new Vector3(closest.transform.position.x, 0, closest.transform.position.z), Quaternion.identity);
            lockIcon.transform.SetParent(closest.transform,false);
            //lockIcon.transform.parent = closest.transform;
            attachments = targetedCreature.GetComponent<Basic_Enemy>().Attachments;
        }
    }

    int position = -1;
    int ChangeValueBy(int change)
    {
        position += change;
        position = position == attachments.Count() ? -1 : position;
        return position;
    }

    public void PreviousMod()
    {
        if (targeted == targetedCreature)
        {
            position = attachments.Count() - 1;
            targeted = attachments[position];
        }
        else
        {
            position = ChangeValueBy(-1);
            if (position == -1)
            {
                lockOn = false;
                targeted = targetedCreature;
            }
            else
            {
                lockOn = true;
                targeted = attachments[position];
            }            
        }
        while (targeted == null)
        {
            PreviousMod();
        }
        Debug.LogError(targeted.name);
        TargetLock(targeted);
    }

    public void NextMod() 
    { 
        if (targeted == targetedCreature)
        {
            position = 0;
            targeted = attachments[position];
        }
        else
        {
            position = ChangeValueBy(1);
            if (position == -1)
            {
                lockOn = false;
                targeted = targetedCreature;
            }
            else
            {
                lockOn = true;
                targeted = attachments[position];
            }
        }
        while (targeted == null)
        {
            NextMod();
        }
        Debug.LogError(targeted.transform.localPosition);
        TargetLock(targeted);
    }

    public void UnLock()
    {
        lockOn = false;
    }
}
