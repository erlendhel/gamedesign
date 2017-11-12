using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    private bool isPressed = false;
    private Vector3 endPosition;

	// Use this for initialization
	void Start () {
        endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player") && !isPressed) {
            transform.position = new Vector3(transform.position.x, 
                transform.position.y, transform.position.z - 0.3f);
            isPressed = true;
        }
    }
}
