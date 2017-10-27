using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudHandler : MonoBehaviour {

    public float incrementX = 0.001f;
    public float incrementZ = 0.002f;

    private float timer = 0.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(incrementX, 0.0f, incrementZ);
        timer += Time.deltaTime;

        if (timer > 40.0f)
        {
            incrementZ *= -1;
            incrementX *= -1;
            timer = 0;
        }
	}
}
