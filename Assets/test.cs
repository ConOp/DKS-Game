using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Awake()
    {
        Modification_Prefab_Manager.LoadModificationPrefabs();
    }
    private void Start()
    {
        GameObject dummy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Dummy"), new Vector3(0, 0.2f, 0), new Quaternion());
        dummy.GetComponent<Dummy>().Add_Attachment(Modification_Factory.Construct_Modification("Speed_Modification_1"));
        dummy.GetComponent<Dummy>().Add_Attachment(Modification_Factory.Construct_Modification("Speed_Modification_1"));

    }
}
