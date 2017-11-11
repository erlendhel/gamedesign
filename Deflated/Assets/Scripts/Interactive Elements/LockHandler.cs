using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockHandler : MonoBehaviour
{
    private bool hasCollidedWithSingleKeyLock = false;
    private bool hasCollidedWithMultiKeyPartsLock = false;
    private Vector3 endPosition;

    public float speed = 5f;

	// Use this for initialization
	void Start ()
    {
        endPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hasCollidedWithSingleKeyLock)
        {
            //Translates the game object over time
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,endPosition, step);
        }

        if (hasCollidedWithMultiKeyPartsLock)
        {
            //Translates the game object over time
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        // Locks that only require one key to be opened
        if (other.gameObject.CompareTag("Player") && PlayerInventory.HasKey() && gameObject.CompareTag("SingleKeyLock"))
        {
            hasCollidedWithSingleKeyLock = true;
        }
        // Locks that require several key parts to be opened
        else if (other.gameObject.CompareTag("Player") && PlayerInventory.HasAllKeyParts() && gameObject.CompareTag("MultiKeyPartsLock"))
        {
            hasCollidedWithMultiKeyPartsLock = true;
        }
    }

}
