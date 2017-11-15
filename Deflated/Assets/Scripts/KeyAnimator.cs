using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimator : MonoBehaviour {

    private float lowerBoundary;
    private float upperBoundary;
    private bool goingUpwards = true;
    private float increment = 0.01f;

	// Use this for initialization
	void Start ()
    {
        upperBoundary = transform.position.y + 0.5f;
        lowerBoundary = transform.position.y;	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (transform.position.y >= upperBoundary)
            goingUpwards = false;
        else if (transform.position.y <= lowerBoundary)
            goingUpwards = true;

        if (goingUpwards)
            transform.Translate(0, increment, 0);
        else
            transform.Translate(0, increment * -1f, 0);

        transform.Rotate(0,increment * 100,0);

    }
}
