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
    [HideInInspector]
    public GameObject targetedCreature;
    [HideInInspector]
    public GameObject targeted;
    List<GameObject> modifications;

    //temp
    public GameObject arrows;
    //endtemp

    public GameObject body;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        Battle currentBattle = Battle_Manager.GetInstance().GetBattle(gameObject);
        if (currentBattle!=null)
        {
            //if user taps on the screen
            if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began)
            {
                SelectEnemy(Input.GetTouch(Input.touchCount - 1),currentBattle.GetEnemies());
            }
            //if no enemy is manually selected
            if (!lockOn)
            {
                //try to select the closest as the target.
                try
                {
                    GameObject target = GetComponent<Player>().Closest(currentBattle.GetEnemies());
                    if (target != targetedCreature)
                    {
                        targetedCreature = target;
                        targeted = targetedCreature;
                        TargetLock(targetedCreature);
                    }
                }catch(Exception e) { }
                
            }
            else
            {
                //if selected enemy exists and the target is on the body
                if (targetedCreature && targeted==targetedCreature)
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

    /// <summary>
    /// checks with raycast if tap is close enough to enemy to trigger selection.
    /// </summary>
    /// <param name="finger"></param>
    /// <param name="enemies"></param>
    void SelectEnemy(Touch finger,List<GameObject> enemies)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.position);
        if(Physics.Raycast(ray,out hit, Mathf.Infinity,LayerMask.GetMask("Ground")))
        {
            GameObject closest = Closest(hit.point,enemies);
            float dist = Vector2.Distance(hit.point, closest.transform.position);
            if (dist < 2.5f)
            {
                lockOn = true;
                targetedCreature = closest;
                targeted = targetedCreature;
            }
        }
    }

    /// <summary>
    /// finds the closest enemy to the tap position.
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="targets"></param>
    /// <returns></returns>
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
            modifications = targetedCreature.GetComponent<Basic_Enemy>().Modification_Bases.GetAllModifications();
        }
    }

    int position = -1;
    int ChangeValueBy(int change)
    {
        position += change;
        position = position >= modifications.Count() ? -1 : position;
        return position;
    }

    public void PreviousMod()
    {
        if (!modifications.Any())
        {
            targeted = targetedCreature;
        }
        else if (targeted == targetedCreature)
        {
            position = modifications.Count() - 1;
            targeted = modifications[position];
        }
        else
        {
            position = ChangeValueBy(-1);
            if (position <= -1)
            {
                position = -1;
                lockOn = false;
                targeted = targetedCreature;
            }
            else
            {
                lockOn = true;
                targeted = modifications[position];
            }            
        }
        while (targeted == null)
        {
            PreviousMod();
        }
        Debug.LogError(targeted.name);
        TargetLock(targeted);
        if (targeted != targetedCreature)
        {
            lockIcon.transform.localScale = new Vector3(1, 1, 1);
            lockIcon.transform.localPosition = new Vector3(-0.5f, 1, 0);
        }
    }

    public void NextMod() 
    {        
        if (!modifications.Any())
        {
            targeted = targetedCreature;
        }
        else if (targeted == targetedCreature)
        {
            position = 0;
            targeted = modifications[position];
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
                targeted = modifications[position];
            }
        }
        while (targeted == null)
        {
            NextMod();
        }        
        Debug.LogError(targeted.transform.localPosition);
        TargetLock(targeted);
        if (targeted != targetedCreature)
        {
            lockIcon.transform.localScale = new Vector3(1, 1, 1);
            lockIcon.transform.localPosition = new Vector3(-0.5f, 1, 0);
        }
    }

    public void UnLock()
    {
        lockOn = false;
    }

    public void RefreshTargets()
    {
        modifications = targetedCreature.GetComponent<Basic_Enemy>().Modification_Bases.GetAllModifications();
        NextMod();
    }
}
