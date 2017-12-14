using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpstreamHandler : MonoBehaviour {

    private Rigidbody rb;
    public float xForce;
    public float yForce;
    public float zForce;
	
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(xForce, yForce, zForce);
        }
    }
}
