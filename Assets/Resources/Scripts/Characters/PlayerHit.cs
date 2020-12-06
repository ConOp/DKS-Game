using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerHit : MonoBehaviour
{
    [HideInInspector]
    Vector3 forward, right;

    public Joystick joystick;

    void Start()
    {
        //calibrate forward and right directions in relation to the camera, NOT the global directions.
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; //sets right position to 90 degrees to the right of forward position.
        StartCoroutine(FindTargetsWithDelay(1.0f));
    }

    
    public float hitRadius = 6;
    [Range(0,360)]
    public float hitAngle = 90;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> hittedTargets = new List<Transform>();

    /// <summary>
    /// searches for target if joystick is dragged more than the sensitivity value.
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            while (Math.Abs(joystick.Horizontal) > 0.2f || Math.Abs(joystick.Vertical) > 0.2f)
            {                
                FindHitableTargets();
                yield return new WaitForSeconds(delay);
            }
            yield return null;
        }        
    }

    /// <summary>
    /// Each second, repositions hit direction according to hit joystick position.
    /// </summary>
    private void Update()
    {
        if (Math.Abs(joystick.Horizontal) > 0.2f || Math.Abs(joystick.Vertical) > 0.2f)
        {
            Vector3 rightMovement = right * Time.deltaTime * joystick.Horizontal;
            Vector3 upMovement = forward * Time.deltaTime * joystick.Vertical;

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;
        }
        else
        {
            transform.forward = transform.parent.forward;
        }
    }

    /// <summary>
    /// searches for hittable targets within the hit radius and hit angle, adds them to list.
    /// </summary>
    void FindHitableTargets()
    {
        hittedTargets.Clear();//clears list everytime it checks to avoid duplicates.
        //holds all the objects in the tagetMast (must be set to Enemies to hold all enemies) who are in reach.
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, hitRadius, targetMask);

        //for each target, check if they are inside cone of attack.
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;//direction of self from target.
            if (Vector3.Angle(transform.forward, dirToTarget) < hitAngle / 2)//target is within the hit angle.
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))//casts a ray from self position, directed towards target and check if passing through obstacles.
                {
                    hittedTargets.Add(target);//if not, then add target to hittedTargets.
                }
            }
        }
        KnockBack(hittedTargets,1);
    }
    
    /// <summary>
    /// Called by FindHittableTargets, knocks each target in hitable list away from source by value and deal 1-5 damage to their hp
    /// </summary>
    /// <param name="hitted"></param>
    void KnockBack(List<Transform> hitted,float value)
    {
        foreach (Transform target in hitted)
        {
            float oldy = target.transform.position.y;
            target.transform.position = Vector3.MoveTowards(target.position,transform.position,-value);//TODO
            target.transform.position = new Vector3(target.position.x,oldy,target.position.z);
            target.GetComponent<Enemy>().TakeDamage(UnityEngine.Random.Range(1,5));            
        }
    }

    //temp?
    public Vector3 DirectionFromAngle(float angleInDeg, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));                
    }
    
}
