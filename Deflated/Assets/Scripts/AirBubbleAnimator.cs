using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubbleAnimator : MonoBehaviour {

    private float yUpperBoundary;
    private float yLowerBoundary;
    private Vector3 startPosition;
    private bool goingUpwards;
    private float increment = 0.009f;

	// Use this for initialization
	void Start ()
    {
        float random = Random.value;
        if (random < 0.1)
            random += 0.2f;
        startPosition = transform.position;
        yUpperBoundary = startPosition.y + random / 2 ;
        yLowerBoundary = startPosition.y - random / 2;
        if (random <= 0.5)
            goingUpwards = true;
        else
            goingUpwards = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (transform.position.y >= yUpperBoundary)
            goingUpwards = false;
        else if (transform.position.y <= yLowerBoundary)
            goingUpwards = true;

        if (goingUpwards)
            transform.Translate(0, increment, 0);
        else
            transform.Translate(0, increment * -1f, 0);        
	}
}
