using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKMovement : MonoBehaviour
{
    public GameObject Chassis;
    public GameObject LegTip1;
    public GameObject LegTip2;
    public GameObject LegTip3;
    public GameObject LegTip4;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public GameObject IKtarget1;
    public GameObject IKtarget2;
    public GameObject IKtarget3;
    public GameObject IKtarget4;
    float journeyTime = 2f;
    bool leg1bool = false;
    bool leg2bool = false;
    bool leg3bool = false;
    bool leg4bool = false;
    float time1;
    float time2;
    float time3;
    float time4;
    Vector3 start1;
    Vector3 end1;
    Vector3 start2;
    Vector3 end2;
    Vector3 start3;
    Vector3 end3;
    Vector3 start4;
    Vector3 end4;


    // Start is called before the first frame update
    void Start()
    {
       LegTip1.transform.position = IKtarget1.transform.position;
       LegTip2.transform.position = IKtarget2.transform.position;
       LegTip3.transform.position = IKtarget3.transform.position;
       LegTip4.transform.position = IKtarget4.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Chassis.transform.position = new Vector3(Chassis.transform.position.x+1*Time.deltaTime,Chassis.transform.position.y,Chassis.transform.position.z);
        if (Vector3.Distance(target1.transform.position, IKtarget1.transform.position) >1.5f && !leg1bool)
        {
            leg1bool = true;
            start1 = IKtarget1.transform.position;
            end1 = target1.transform.position;
            time1 = Time.time;
        }
        else if (Vector3.Distance(target2.transform.position, IKtarget2.transform.position) > 1.5f && !leg2bool)
        {
            leg2bool = true;
            start2 = IKtarget2.transform.position;
            end2 = target2.transform.position;
            time2 = Time.time;
        }
        else if (Vector3.Distance(target3.transform.position, IKtarget3.transform.position) > 1.5f && !leg3bool)
        {
            leg3bool = true;
            start3 = IKtarget3.transform.position;
            end3 = target3.transform.position;
            time3 = Time.time;
        }
        else if (Vector3.Distance(target4.transform.position, IKtarget4.transform.position) > 1.5f && !leg4bool)
        {
            leg4bool = true;
            start4 = IKtarget4.transform.position;
            end4 = target4.transform.position;
            time4 = Time.time;
        }
        if (leg1bool)
        {
            Vector3 centerPoint = (IKtarget1.transform.position + end1) / 2f;
            centerPoint -= Vector3.up;
            Vector3 startRelCenter = IKtarget1.transform.position - centerPoint;
            Vector3 endRelCenter = end1 - centerPoint;
            IKtarget1.transform.position = Vector3.Slerp(startRelCenter, endRelCenter, (Time.time - time1) / journeyTime);
            IKtarget1.transform.position += centerPoint;
            if (IKtarget1.transform.position == end1)
            {
                leg1bool = false;
            }
            else
            {
                leg1bool = true;
            }
        }
        if (leg2bool)
        {
            Vector3 centerPoint = (IKtarget2.transform.position + end1) / 2f;
            centerPoint -= Vector3.up;
            Vector3 startRelCenter = IKtarget2.transform.position - centerPoint;
            Vector3 endRelCenter = end2 - centerPoint;
            IKtarget2.transform.position = Vector3.Slerp(startRelCenter, endRelCenter, (Time.time - time2) / journeyTime);
            IKtarget2.transform.position += centerPoint;
            if (IKtarget2.transform.position == end2)
            {
                leg2bool = false;
            }
            else
            {
                leg2bool = true;
            }
        }
        if (leg3bool)
        {
            Vector3 centerPoint = (IKtarget3.transform.position + end3) / 2f;
            centerPoint -= Vector3.up;
            Vector3 startRelCenter = IKtarget3.transform.position - centerPoint;
            Vector3 endRelCenter = end3 - centerPoint;
            IKtarget3.transform.position = Vector3.Slerp(startRelCenter, endRelCenter, (Time.time - time3) / journeyTime);
            IKtarget3.transform.position += centerPoint;
            if (IKtarget3.transform.position == end3)
            {
                leg3bool = false;
            }
            else
            {
                leg3bool = true;
            }
        }
        if (leg4bool)
        {
            Vector3 centerPoint = (IKtarget4.transform.position + end4) / 2f;
            centerPoint -= Vector3.up;
            Vector3 startRelCenter = IKtarget4.transform.position - centerPoint;
            Vector3 endRelCenter = end4 - centerPoint;
            IKtarget4.transform.position = Vector3.Slerp(startRelCenter, endRelCenter, (Time.time - time4) / journeyTime);
            IKtarget4.transform.position += centerPoint;
            if (IKtarget4.transform.position == end4)
            {
                leg4bool = false;
            }
            else
            {
                leg4bool = true;
            }
        }
    }
}
