using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRockHandler : MonoBehaviour {
    public bool isActivated = false;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player") && isActivated == false) {
            isActivated = true;
        }
    }
}
