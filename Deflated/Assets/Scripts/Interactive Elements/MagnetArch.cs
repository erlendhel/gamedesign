using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MagnetArch : MonoBehaviour
{

    public GameObject gameObject;
    public Rigidbody rb;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(0f, 15f, 0f);
        }
    }


}
