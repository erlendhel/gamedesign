using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Transform bucket;

    private float lastBucketY;

	// Use this for initialization
	void Start ()
    {
        
        lastBucketY = bucket.transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float currentBucketY = bucket.transform.position.y;
        float yToTranslate = (currentBucketY - lastBucketY) * -1;

        transform.Translate(0, yToTranslate, 0);
        
        lastBucketY = bucket.transform.position.y;
    }
}
