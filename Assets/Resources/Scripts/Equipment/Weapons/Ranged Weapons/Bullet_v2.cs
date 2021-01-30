using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_v2 : MonoBehaviour
{
    [HideInInspector]
    public float range = 10f;
    [HideInInspector]
    public float speed = 10f;
    [HideInInspector]
    public float damage = 10f;
    Vector3 initpos;
    public GameObject effect;

    [HideInInspector]
    public GameObject target;

    private void Start()
    {
        initpos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, initpos) >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //if target is a child of the collided object (aka the target is a modification)
            if(target.transform.parent == other.gameObject.transform)
            {
                target.GetComponent<Modification>().TakeDamage(damage*0.9f);
                other.GetComponent<Basic_Enemy>().TakeDamage(damage*0.1f);
            }
            else
            {
                other.GetComponent<Basic_Enemy>().TakeDamage(damage);
            }
            GameObject flash = GameObject.Instantiate(effect, transform.position, Quaternion.identity);
            flash.transform.forward = -transform.forward;
        }
        if(other.tag == "Player")
        {
            return;
        }
        Destroy(gameObject);
    }

}
