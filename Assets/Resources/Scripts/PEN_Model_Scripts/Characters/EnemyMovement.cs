using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{

    public Transform target;
    public float speed = 0.01f;
    GameObject manager;
    private void Start()
    {        
        manager = GameObject.Find("Manager");
    }

    public void Move()
    {
        //gets closestPlayer to enemy from list of players
        GameObject closestPlayer = manager.GetComponent<Manager>().Closest(gameObject.transform.position, manager.GetComponent<Manager>().players);
        transform.position = Vector3.MoveTowards(transform.position, closestPlayer.transform.position, 0.04f);
    }

}
