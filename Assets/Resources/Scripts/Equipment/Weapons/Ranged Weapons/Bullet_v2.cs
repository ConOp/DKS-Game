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
            other.GetComponent<Basic_Enemy>().TakeDamage(damage);
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
