using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool isGrounded = true;
    public bool inWater = false;
    public Rigidbody rb;

    public float speed = 15f;
    public float jumpSpeed = 500.0f;
    public float vel;

    // Default values of game character
    public float initMass;
    public Vector3 initScale;

    public Vector3 initGrav = new Vector3(0, -9.81f, 0);

    public Vector3 spawnPosition;

    public bool hasKey = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        initScale = transform.localScale;
        initMass = rb.mass;
        spawnPosition = rb.position;
        Physics.gravity = initGrav;
    }

    private void Update() {

        vel = rb.velocity.magnitude;

        if (Input.GetKeyDown("space") && isGrounded) {
            Jump();
        }

    }

    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Make player follow the direction of the camera
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0.0f;

        movement = Vector3.Normalize(movement);
        
        if (inWater == false) {
            rb.AddForce(movement * speed);
        } else {
            rb.AddForce(movement * (speed / 3));
        }
    }

    /*
     *  Function which adds force upwards, thus producing a jump.
     *  The variable jumpSpeed can be altered to produce effects
     *  of buffs, pickups etc.  
     */
    private void Jump() {
        rb.AddForce(Vector3.up * jumpSpeed);
    }

    /*
     *  Functions 'OnCollisionEnter' and 'OnCollisionExit' detects
     *  if the game character is on or off the ground. Used to prevent
     *  weird in-air behaviour such as double jumps.
     */
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Terrain")) {
            isGrounded = true;
        }
        if (collision.gameObject.name == "Water") {
            inWater = true;
        }
        if (collision.gameObject.CompareTag("BouncyMat")) {
            rb.AddForce(Vector3.up * 1400);
        }
    }

    // Function to detect if the game character is off the ground
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Terrain")) {
            isGrounded = false;
        } else if (collision.gameObject.name == "Water") {
            inWater = false;
        }
    }

    public Vector3 getSpawnPosition() {
        return spawnPosition;
    }
}