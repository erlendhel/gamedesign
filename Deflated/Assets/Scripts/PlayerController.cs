using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float jumpSpeed = 200.0f;
    private bool isGrounded = true;
    private bool inWater = false;
    private Rigidbody rb;

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

        movement = Camera.main.transform.TransformDirection(movement);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Minimizer"))
        {
            other.gameObject.SetActive(false);
            transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
            rb.mass = (rb.mass / 2);
        }
    }
}