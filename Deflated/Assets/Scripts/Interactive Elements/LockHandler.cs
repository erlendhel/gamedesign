using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class LockHandler : MonoBehaviour
{
    private bool hasCollidedWithSingleKeyLock = false;
    private bool hasCollidedWithMultiKeyPartsLock = false;
    private Vector3 endPosition;
    private float timer = 0f;
    private bool timerActive = false;
    private Animator anim;
    private bool doorHasMoved = false;

    public GameObject message;
    public float speed = 5f;

	// Use this for initialization
	void Start ()
    {
        endPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        anim = GetComponent<Animator>();
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
        
        // Hides the message text after 3 seconds 
        if (timerActive && timer >= 3.0f) {
            message.SetActive(false);
            timerActive = false;
            timer = 0f;
        }
	}

    private void FixedUpdate()
    {
        // Update timer when it has been activted
        if (timerActive)
            timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Locks that only require one key to be opened
        if (other.gameObject.CompareTag("Player") && PlayerInventory.HasKey() && gameObject.CompareTag("SingleKeyLock"))
        {
            hasCollidedWithSingleKeyLock = true;
        }
        // Show a message if the players collides with the door without all key parts
        else if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("MultiKeyPartsLock") && !PlayerInventory.HasAllKeyParts()) 
        {
            Debug.Log("Collect all key parts!");
            message.SetActive(true);
            timerActive = true;
        }
        // Locks that require several key parts to be opened
        else if (other.gameObject.CompareTag("Player") && PlayerInventory.HasAllKeyParts() && gameObject.CompareTag("MultiKeyPartsLock"))
        {

            // First time the door is hit when player has all keys, play animation
            if (!doorHasMoved)
            {
                anim.Play("Door");
                doorHasMoved = true;
            }
        }
    }
}
