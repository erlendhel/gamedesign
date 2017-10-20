using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 15f;
    public float jumpSpeed = 200.0f;
    public float pickupConstant = 2.0f;
    private bool isGrounded = true;
    private bool inWater = false;
    private Rigidbody rb;

    // These values need to coincide with the scale of the character set in Unity
    private float x_size = 1.0f;
    private float y_size = 1.0f;
    private float z_size = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (isGrounded == true && inWater == false)
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Make player follow the direction of the camera
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0.0f;

        movement = Vector3.Normalize(movement);
        
        if (inWater == false)
        {
            rb.AddForce(movement * speed);
        }
        else
        {
            rb.AddForce(movement * (speed / 3));
        }
    }

    /*
     *  Function which adds force upwards, thus producing a jump.
     *  The variable jumpSpeed can be altered to produce effects
     *  of buffs, pickups etc.  
     */
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpSpeed);
    }

    /*
     *  Functions 'OnCollisionEnter' and 'OnCollisionExit' detects
     *  if the game character is on or off the ground. Used to prevent
     *  weird in-air behaviour such as double jumps.
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            isGrounded = true;
        }
        if (collision.gameObject.name == "Water")
        {
            inWater = true;
        }
    }

    // Function to detect if the game character is off the ground
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            isGrounded = false;
        }
        if (collision.gameObject.name == "Water")
        {
            inWater = false;
        }
    }

    // Function to detect if the game character goes over a pickup.
    private void OnTriggerEnter(Collider other)
    {
        // Checking if the player collides with a minimizer pickup
        if (other.gameObject.CompareTag("Minimizer"))
        {
            // If a player picks up the minimizer, the minimizer object is set to inactive
            other.gameObject.SetActive(false);
            
            // Transform the scale in terms of the ORIGINAL size of the character
            transform.localScale = new Vector3(x_size / 2, y_size / 2, z_size / 2);
            // Divide the mass by 2
            rb.mass = (rb.mass / 2);
        }
        if (other.gameObject.CompareTag("Maximizer"))
        {
            // If a player picks up the minimizer, the minimizer object is set to inactive
            other.gameObject.SetActive(false);
            // Transform the scale in terms of the ORIGINAL size of the character
            transform.localScale = new Vector3(x_size * 2, y_size * 2, z_size * 2);
            // Divide the mass by 2
            rb.mass = (rb.mass * 2);
        }
    }
}