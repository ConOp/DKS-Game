using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    const float SPEED = 30f;
    public float DAMAGE = 100f;
    Vector3 target;

    // Update is called once per frame
    void Update()
    {
        // target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Dummy>().Attachments[0].transform.position;
        float step = SPEED * Time.deltaTime;
        transform.position += transform.forward * step;
    }
}
