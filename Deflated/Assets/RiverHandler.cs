using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverHandler : MonoBehaviour {

    private Rigidbody rb;
    public float xForce;
    public float yForce;
    public float zForce;

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(xForce, yForce, zForce);
        }
    }
}
