using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockHandler : MonoBehaviour
{

    private PlayerController playerHandler;
    public GameObject player;
    private bool hasCollidedWithLock = false;
    private Vector3 endPosition;

    public float speed = 5f;

	// Use this for initialization
	void Start ()
    {
        playerHandler = player.GetComponent<PlayerController>();
        endPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hasCollidedWithLock)
        {
            //Translates the game object over time
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,endPosition, step);
        }		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && playerHandler.hasKey)
        {
            hasCollidedWithLock = true;
            playerHandler.hasKey = false;
        }
    }

}
