using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopKeyPartAnimator : MonoBehaviour {

    private float lowerBoundary;
    private float upperBoundary;
    private bool goingUpwards = true;
    private float increment = 0.009f;

    // Use this for initialization
    void Start()
    {
        upperBoundary = transform.position.x + 0.025f;
        lowerBoundary = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x >= upperBoundary)
            goingUpwards = false;
        else if (transform.position.x <= lowerBoundary)
            goingUpwards = true;

        if (goingUpwards)
            transform.Translate(increment, 0, 0);
        else
            transform.Translate(increment * -1f, 0, 0);

        transform.Rotate(increment * 100f, 0, 0);
    }
}
