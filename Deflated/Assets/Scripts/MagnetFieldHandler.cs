using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetFieldHandler : MonoBehaviour {

    public GameObject gameObject;
    public RigidBody rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    private void OnCollisionStay(Collision collision)
    {
        if (collision.GameObject.CompareTag("Player"))
        {
            rb = collision.GetComponent<RigidBody>();
            rb.AddForce(10.0f, 10.0f, 10.0f);
        }
    }
}
