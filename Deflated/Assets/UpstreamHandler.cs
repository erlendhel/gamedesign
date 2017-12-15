using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpstreamHandler : MonoBehaviour {

    private Rigidbody rb;
    public float xForce = 0;
    public float yForce = 0;
    public float zForce = 0;
	
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(xForce, yForce, zForce);
        }
    }
}
