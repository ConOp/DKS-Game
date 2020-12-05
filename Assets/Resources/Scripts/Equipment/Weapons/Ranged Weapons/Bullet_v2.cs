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
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
