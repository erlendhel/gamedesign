using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockHandler : MonoBehaviour
{

    private PlayerController playerHandler;
    public GameObject player;
    private bool hasCollided = false;
    private Vector3 startPosition;
    private Vector3 endPosition;

    float startTime;

	// Use this for initialization
	void Start ()
    {
        playerHandler = player.GetComponent<PlayerController>();
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x, startPosition.y + 2f, startPosition.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hasCollided)
        {
            //Translates the game object over time with constant speed
            float currentDuration = Time.time - startTime;
            float totalDistance = Vector3.Distance(startPosition, endPosition);
            float journeyFraction = currentDuration / totalDistance;
            transform.position = Vector3.Lerp(startPosition, endPosition, journeyFraction);
        }		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && playerHandler.hasKey)
        {
            hasCollided = true;
            playerHandler.hasKey = false;
            startTime = Time.time;
        }
    }

}
