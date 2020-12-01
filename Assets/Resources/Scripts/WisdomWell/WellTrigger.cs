using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellTrigger : MonoBehaviour
{
    public GameObject mark;
    public GameObject peerButton;
    // Start is called before the first frame update
    void Start()
    {
        peerButton.SetActive(false);
        mark.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mark.SetActive(true);
            peerButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mark.SetActive(false);
            peerButton.SetActive(false);
        }
    }

    public void WellSpeak()
    {

    }
}
