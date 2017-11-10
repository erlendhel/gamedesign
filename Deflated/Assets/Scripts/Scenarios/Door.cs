using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Transform bucket;

    private float lastBucketY;
    private bool isMoving = false;
    private Rigidbody bucketRigidBody;

	// Use this for initialization
	void Start ()
    {
        lastBucketY = bucket.transform.position.y;
        bucketRigidBody = bucket.gameObject.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        float currentBucketY = bucket.transform.position.y;
        float yToTranslate = (currentBucketY - lastBucketY) * -1;
        
        // Used to determine if a sound effect should be played. 
        if (bucketRigidBody.velocity.magnitude >= 0.1f)
            isMoving = true;
        else
            isMoving = false;

        // If the door is moving, and the SFX hasn't started playing, start playing SFX
        if (isMoving && !AudioManager.instance.IsPlaying("PulleyOperating"))
            AudioManager.instance.Play("PulleyOperating");
        // If door has stopped moving, and the SFX is still playing, stop SFX
        else if (!isMoving && AudioManager.instance.IsPlaying("PulleyOperating"))
            AudioManager.instance.Stop("PulleyOperating");

        transform.Translate(0, yToTranslate, 0);
        lastBucketY = bucket.transform.position.y;
    }
}
