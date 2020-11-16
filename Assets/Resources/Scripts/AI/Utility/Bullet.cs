using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
    private void OnTriggerEnter(Collider collision)
    {

        
        if (collision.gameObject.GetComponent<Modification>() != null)
        {
            if (collision.gameObject.GetComponent<Modification>().TakeDamage(DAMAGE) && collision.gameObject.transform.parent!=null)
            {
                collision.gameObject.transform.parent.GetComponent<Basic_Enemy>().Remove_Modification(collision.gameObject);
            }
            Destroy(gameObject);
        }
        else if(collision.gameObject.GetComponent<Modification>() == null && collision.gameObject.transform.parent.GetComponent<Basic_Enemy>() != null)
        {
            if (collision.gameObject.transform.parent.GetComponent<Basic_Enemy>().TakeDamage(DAMAGE))
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
