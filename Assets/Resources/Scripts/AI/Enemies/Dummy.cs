using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy:Basic_Enemy
{
    GameObject target;
    bool dashing = false;
    bool stunned = false;
    float attackTimer = 6f;
    float cdTime = 0;

    public  override void InitializeModificationBases()
    {
        Attachment_Bases = new List<(Transform, string,string,bool)>();
        foreach (Transform child in transform)
        {
            if (child.name == "Attachment1")
            {
                Attachment_Bases.Add((child, "Medium","E",false));
            }
            else if (child.name == "Attachment2")
            {
                Attachment_Bases.Add((child, "Medium","W",false));
            }
            else if (child.name == "Attachment3")
            {
                Attachment_Bases.Add((child, "Large","N",false));
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
    }
    private void Update()
    {
        if (!stunned)
        { 
            if (cdTime >= attackTimer - (attackTimer * (current_attack_speed-ATTACK_SPEED)))
            {
                dashing = true;
                cdTime = 0;
            }
            if (dashing)
            {
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
            else
            {
                cdTime += Time.deltaTime;
            }
        }
    }
    /// <summary>
    /// Follows the target.
    /// </summary>
    /// <param name="target"></param>
    public void MoveToTarget(GameObject target)
    {
        float step = max_movement_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            if (dashing)
            {
                current_speed = 0;
                stunned = true;
                dashing = false;
                cdTime = 0;
                Invoke("Unstun", 3f);
            }
        }
    }
    /// <summary>
    /// Enemy gets stunned unable to continue it's attacks.
    /// </summary>
    void Unstun()
    {
        stunned = false;
    }

}
