using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy:MonoBehaviour
{
    const float MOVEMENT_SPEED= 1;
    const float HEALTH= 50;
    const float DAMAGE = 50;

    float current_health = HEALTH;
    float current_movement_speed = MOVEMENT_SPEED;
    float current_damage = DAMAGE;
    public List<(Vector3 base_position, string base_size)> Attachment_Bases;
    public List<(GameObject gobject, IModification stats)> Attachments;
    GameObject target;
    public  void InitializeAttachmentBases()
    {
        Attachment_Bases = new List<(Vector3, string)>();
        foreach (Transform child in transform)
        {
            if (child.name == "Attachment1")
            {
                Attachment_Bases.Add((child.transform.position, "Medium"));
            }
            else if (child.name == "Attachment2")
            {
                Attachment_Bases.Add((child.transform.position, "Medium"));
            }
            else if (child.name == "Attachment3")
            {
                Attachment_Bases.Add((child.transform.position, "Large"));
            }
        }
    }
    private void Awake()
    {
        InitializeAttachmentBases();
        Attachments = new List<(GameObject, IModification)>();
    }
    private void Start()
    {
        target=GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position)>1)
        {
            MoveToTarget(target);
        }
        Debug.Log(current_movement_speed);

    }
    public void MoveToTarget(GameObject target)
    {
        float step = current_movement_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
    public void Add_Attachment(IModification mod)
    {
        for(int i = 0; i < Attachment_Bases.Count; i++)
        {
            if (Attachment_Bases[i].base_size == mod.size)
            {
                GameObject modification = Instantiate(Modification_Prefab_Manager.SearchModification(mod.type),Attachment_Bases[i].base_position,new Quaternion(),this.transform);
                modification.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
                Attachments.Add((modification, mod));
                Attachment_Bases.RemoveAt(i);
                Appy_Modification_Boosts();
                return;
            }
        }
        return;
    }
    public void Appy_Modification_Boosts()
    {
        float totalspeedboost = 0;
        for(int i = 0; i < Attachments.Count; i++)
        {
            totalspeedboost += Attachments[i].stats.movement_speed_boost;
        }
        if (totalspeedboost > 0)
        {
            current_movement_speed = MOVEMENT_SPEED * totalspeedboost;
        }
        else
        {
            current_movement_speed = MOVEMENT_SPEED;
        }
    }
}
