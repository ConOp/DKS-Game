using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{

    public Transform target;
    public float speed = 0.01f;

    // Update is called once per frame
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.02f);
    }
}
