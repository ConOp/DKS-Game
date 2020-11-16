using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged :Basic_Enemy
{
    public GameObject target;
    public GameObject bulletObject;
    public bool attack = false;
    public bool stunned = false;
    float attackTimer = 6f;
    float cdTime = 0;
    public override void InitializeModificationBases()
    {
        Attachment_Bases = new List<(Transform, string, string,bool)>();
        foreach (Transform child in transform)
        {
            if (child.name == "Attachment1")
            {
                Attachment_Bases.Add((child, "Medium","N", false));
            }
            else if (child.name == "Attachment2")
            {
                Attachment_Bases.Add((child, "Medium","E", false));
            }
            else if (child.name == "Attachment3")
            {
                Attachment_Bases.Add((child, "Medium","W", false));
            }
        }
    }
    private void Awake()
    {
        InitializeModificationBases();
        Attachments = new GameObject[Attachment_Bases.Count];
        gameObject.GetComponent<SphereCollider>().radius = current_range*0.8f;
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!stunned)
        {
            Vector3 TargetDirection = target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(TargetDirection.x, 0, TargetDirection.z), 0.1f, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            cdTime += Time.deltaTime;
            if (Vector3.Distance(transform.position, target.transform.position) > current_range)
            {
                transform.Translate(Vector3.forward * max_movement_speed * Time.deltaTime);
            }
            else
            {
                if (cdTime >= attackTimer - (attackTimer * (current_attack_speed - ATTACK_SPEED)))
                {
                    attack = true;
                    cdTime = 0;
                }
                if (attack)
                {
                    Shoot();
                    attack = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Vector3 direction = transform.position - other.gameObject.transform.position;
            transform.Translate(Vector3.back * max_movement_speed * Time.deltaTime);

        }
    }
    /// <summary>
    /// Shoot Player.
    /// </summary>
    void Shoot()
    {
        GameObject bullet=Instantiate(bulletObject, transform.position, new Quaternion());
        bullet.transform.rotation = gameObject.transform.rotation;
    }
    
  
}

