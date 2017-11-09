using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public Transform bucket;

    private float lastBucketY;
    private bool isMoving = false;

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

        // Used to determine if a sound effect should be played. 
        // Bigger than or equal to 0.01f to ensure no audio at start of scene
        if (yToTranslate >= 0.001f)
        {
            Debug.Log("isMoving = true");
            isMoving = true;
        }
        else
        {
            Debug.Log("isMoving = true");
            isMoving = false;
        }
            

        Debug.Log(yToTranslate);

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
