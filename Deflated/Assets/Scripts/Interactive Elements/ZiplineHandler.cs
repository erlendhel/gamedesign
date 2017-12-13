using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiplineHandler : MonoBehaviour {

    public Animator anim;
    public GameObject player;
    private HingeJoint hinge;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        hinge = GetComponent<HingeJoint>();
	}

    private void Update() {
        if (Input.GetKeyDown("space") && hinge.connectedBody != null) {
            hinge.connectedBody = null;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hinge.connectedBody = player.gameObject.GetComponent<Rigidbody>();
            anim.Play("ZiplineAnim");
        }
    }
}
