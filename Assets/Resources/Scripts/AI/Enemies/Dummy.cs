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
    public List<(Transform base_position, string base_size,bool used)> Attachment_Bases;
    public GameObject[] Attachments;
    GameObject target;
    public  void InitializeAttachmentBases()
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
        InitializeAttachmentBases();
        Attachments = new GameObject[Attachment_Bases.Count];
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
        //Debug.Log(current_movement_speed);

    }
    
    public void MoveToTarget(GameObject target)
    {
        float step = current_movement_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }
    public void Add_Attachment(string mod_type)
    {
        Modification mod = Modification_Prefab_Manager.SearchModification(mod_type).GetComponent<Modification>();
        for(int i = 0; i < Attachment_Bases.Count; i++)
        {
            if (Attachment_Bases[i].base_size == mod.size && !Attachment_Bases[i].used)
            {
                GameObject modification = Instantiate(Modification_Prefab_Manager.SearchModification(mod.type),Attachment_Bases[i].base_position.transform.position,new Quaternion(),gameObject.transform);
                //modification.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
                Attachments[i]=modification;
                Attachment_Bases[i] = (Attachment_Bases[i].base_position, Attachment_Bases[i].base_size, true);
                Appy_Modification_Boosts();
                return;
            }
        }
        return;
    }
    public void Appy_Modification_Boosts()
    {
        float totalspeedboost = 0;
        for(int i = 0; i < Attachments.Length; i++)
        {
            if (Attachments[i] != null)
            {
                totalspeedboost += Attachments[i].GetComponent<Modification>().movement_speed;
            }
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
    public void Remove_Attachment(GameObject mod_to_remove)
    {
        int index = -1;
        for(int i=0; i < Attachments.Length; i++)
        {
            if (Attachments[i] == mod_to_remove)
            {
                index = i;
            }
        }
        if (index == -1)
        {
            return;
        }
        else
        {
            //Destroy(Attachments[index].GetComponent<FixedJoint>());
            Attachments[index].transform.parent = null;
            Attachments[index].GetComponent<Rigidbody>().isKinematic = false;
            Attachments[index].GetComponent<Collider>().isTrigger = false;
            Attachments[index].gameObject.GetComponent<Rigidbody>().AddExplosionForce(5f, Attachments[index].transform.position, 3f, 0f, ForceMode.Impulse);
            Attachments[index] = null;
            Attachment_Bases[index] = (Attachment_Bases[index].base_position, Attachment_Bases[index].base_size, false);
            Appy_Modification_Boosts();
        }
    }
}
