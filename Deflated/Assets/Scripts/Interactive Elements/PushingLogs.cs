using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingLogs : MonoBehaviour {

    public GameObject gameObject;
    public Rigidbody rb;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(0f, 25.0f, -1400f);
        }
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Log"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(0f, 25f, 1400f);
        }
    }
}

