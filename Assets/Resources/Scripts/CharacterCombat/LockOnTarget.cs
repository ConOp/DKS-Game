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
            targeted = SelectEnemy(Input.GetTouch(Input.touchCount - 1));
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

    GameObject SelectEnemy(Touch finger)
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(finger.position);
        Vector2 touchPos2d = new Vector2(touchPos.x, touchPos.y);
        
        RaycastHit2D hitInfo = Physics2D.Raycast(touchPos2d, Camera.main.transform.forward);

        if (hitInfo != null)
        {
            GameObject closest = mng.Closest(touchPos, mng.enemies);
            float dist = Vector2.Distance(touchPos2d, new Vector2(closest.transform.position.x, closest.transform.position.z));
            if (dist < 3)
            {
                lockOn = true;
                return closest;
            }
        }
        /*
        GameObject closest = mng.Closest(finger.position, mng.enemies);
        if (Vector2.Distance(new Vector2(finger.position.x,finger.position.y), new Vector2(closest.transform.position.x,closest.transform.position.z)) < 1)
        {
            lockOn = true;
            return closest;
        }
        */
        return null;
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
