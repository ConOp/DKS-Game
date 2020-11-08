using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy:Basic_Enemy
{
    GameObject target;
    float current_speed = 1;
    bool dashing = false;
    bool stunned = false;
    public  override void InitializeModificationBases()
    {
        Attachment_Bases = new List<(Transform, string,bool)>();
        foreach (Transform child in transform)
        {
            if (child.name == "Attachment1")
            {
                Attachment_Bases.Add((child, "Medium",false));
            }
            else if (child.name == "Attachment2")
            {
                Attachment_Bases.Add((child, "Medium",false));
            }
            else if (child.name == "Attachment3")
            {
                Attachment_Bases.Add((child, "Large",false));
            }
        }
    }
    private void Awake()
    {
        InitializeModificationBases();
        Attachments = new GameObject[Attachment_Bases.Count];
    }
    private void Start()
    {
        target=GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("DashAttack", 1f, 6f);
    }
    private void Update()
    {
        if (dashing && !stunned)
        {
            DashAttack();
        }
        if (dashing)
        {
            Debug.Log("Speed: " + current_speed);
            if (current_speed <= max_movement_speed)
            {
                current_speed += max_movement_speed * 0.83f / 100;
            }
            else
            {
                current_speed = max_movement_speed;
            }
            Vector3 TargetDirection = target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(TargetDirection.x, 0, TargetDirection.z), 0.2f / current_speed, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            transform.position += transform.forward * current_speed * Time.deltaTime;
        }
    }
    public void MoveToTarget(GameObject target)
    {
        float step = max_movement_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            Debug.Log("Stunned");
            if (dashing)
            {
                current_speed = 0;
                dashing = false;
                stunned = true;
                Invoke("Unstun", 3f);
            }
        }
    }
    public void DashAttack()
    {
        if (!stunned)
        {
            dashing = true;
        }
    }
    void Unstun()
    {
        Debug.Log("UNstunned");
        stunned = false;
    }

}
