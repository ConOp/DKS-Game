using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged :Basic_Enemy
{
    public GameObject target;
    public float range;
    public GameObject bulletObject;
    public bool attack = false;
    public override void InitializeModificationBases()
    {
        Attachment_Bases = new List<(Transform, string, bool)>();
        foreach (Transform child in transform)
        {
            if (child.name == "Attachment1")
            {
                Attachment_Bases.Add((child, "Medium", false));
            }
            else if (child.name == "Attachment2")
            {
                Attachment_Bases.Add((child, "Medium", false));
            }
            else if (child.name == "Attachment3")
            {
                Attachment_Bases.Add((child, "Medium", false));
            }
        }
    }
    private void Awake()
    {
        InitializeModificationBases();
        Attachments = new GameObject[Attachment_Bases.Count];
        gameObject.GetComponent<SphereCollider>().radius = range*0.9f;
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("shootattack", 3f, 6f);
    }

    private void Update()
    {

        Debug.Log(max_movement_speed);
            Vector3 TargetDirection = target.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward,new Vector3(TargetDirection.x,0,TargetDirection.z), 0.1f, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        if (attack)
        {
            Shoot();
            attack = false;
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

    void Shoot()
    {
        GameObject bullet=Instantiate(bulletObject, transform.position, new Quaternion());
        bullet.transform.rotation = gameObject.transform.rotation;
    }
    
    void shootattack()
    {
        attack = true;
    }
  
}

