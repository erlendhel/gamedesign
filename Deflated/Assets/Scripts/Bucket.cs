using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {

    private float yStartPosition;
    private Rigidbody bucketRigidBody;

	// Use this for initialization
	void Start ()
    {
        yStartPosition = transform.position.y;
        bucketRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y < yStartPosition)
        {
            bucketRigidBody.AddForce(0, 1, 0);
        }

	}
}
