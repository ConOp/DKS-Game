using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    bool lockOn = false;
    Manager mng;
    GameObject same = null;
    GameObject arrow;
    GameObject targeted;

    public GameObject terrain;
    public GameObject arrows;

    private void Start()
    {
        mng = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount-1).phase == TouchPhase.Began)
        {
            SelectEnemy(Input.GetTouch(Input.touchCount - 1));
        }
        if (!lockOn)
        {
            targeted = mng.Closest(this.gameObject.transform.position, mng.enemies);
            TargetLock(targeted);
        }
        else
        {
            TargetLock(targeted);
        }
    }

    void SelectEnemy(Touch finger)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(finger.position);
        if(terrain.GetComponent<Collider>().Raycast(ray,out hit, Mathf.Infinity))
        {
            GameObject closest = mng.Closest(hit.point, mng.enemies);
            float dist = Vector2.Distance(hit.point, closest.transform.position);
            if (dist < 3)
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
