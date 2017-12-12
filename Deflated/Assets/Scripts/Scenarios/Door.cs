using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Transform bucket;

    private float lastBucketY;
    private bool isMoving = false;
    private Rigidbody bucketRigidBody;

	void Start ()
    {
        lastBucketY = bucket.transform.position.y;
        bucketRigidBody = bucket.gameObject.GetComponent<Rigidbody>();
    }
	
	void Update()
    {
        // Keep track of the bucket position
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

        //Translate the door by how much the bucket have moved since last update
        transform.Translate(0, yToTranslate, 0);
        lastBucketY = bucket.transform.position.y;
    }
}
