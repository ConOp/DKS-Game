using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public void PickedUp()
    {
        gameObject.GetComponent<EquipmentOnGround>().enabled = false;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 20, gameObject.transform.position.z);
        Destroy(gameObject,0.2f);
    }
}
