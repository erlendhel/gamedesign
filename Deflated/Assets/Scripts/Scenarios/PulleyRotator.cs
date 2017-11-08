using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyRotator : MonoBehaviour {

    public Transform objectConnectedTo;
    public bool clockwise = false;

    private float lastDoorY;
    

    // Use this for initialization
    void Start()
    {
        lastDoorY = objectConnectedTo.transform.position.y;
    }

    // Update is called once per frame
    void Update ()
    {
        float currentDoorY = objectConnectedTo.transform.position.y;
        float toRotate = (currentDoorY - lastDoorY) * -1;

        if (clockwise)
            toRotate *= -1;

        transform.Rotate(0, toRotate * 100, 0);

        lastDoorY = objectConnectedTo.position.y;
	}
}
