using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 15f;
    public float jumpSpeed = 100.0f;
    private bool isGrounded = true;
    private bool inWater = false;
    private Rigidbody rb;

    // Default values of game character, NOT directly linked with the ones in Unity.
    private float x_size = 1.0f;
    private float y_size = 1.0f;
    private float z_size = 1.0f;
    private float mass = 1.0f;

    // Variables used to determine states of pickups
    public bool hasMinimizer = false;
    public bool hasMaximizer = false;
    private bool minimizerActive = false;
    private bool maximizerActive = false;
    private int pickupDuration = 5;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetKeyDown("space") && isGrounded == true && inWater == false) {
            Jump();
        }
        if (Input.GetKeyDown("1") && hasMinimizer == true) {
            activateMinimizer();
        }
        if (Input.GetKeyDown("2") && hasMaximizer == true) {
            activateMaximizer();
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
        if (collision.gameObject.name == "Terrain") {
            isGrounded = true;
        }
        if (collision.gameObject.name == "Water") {
            inWater = true;
        }
    }

    // Function to detect if the game character is off the ground
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.name == "Terrain") {
            isGrounded = false;
        } else if (collision.gameObject.name == "Water") {
            inWater = false;
        }
    }
    
    // Function to detect if the game character goes over a pickup.
    private void OnTriggerEnter(Collider other) {
        // Checking if the player collides with a minimizer pickup
        if (other.gameObject.CompareTag("Minimizer")) {
            other.gameObject.SetActive(false);  // Set minimizer object to inactive
            hasMinimizer = true; // Player has acquired a minimizer pickup
        } else if (other.gameObject.CompareTag("Maximizer")) {
            other.gameObject.SetActive(false); // Set maximizer object to inactive
            hasMaximizer = true; // Player has acquired a maximizer pickup
        }
    }

    // Function that activates the minimizer-pickup
    private void activateMinimizer() {
        // Check if a maximizer-pickup is activated, if it has, stop the current coroutine
        if (maximizerActive == true) {
            StopCoroutine("maxminTimer");
        }
        // Apply the buff from the minimizer
        transform.localScale = new Vector3(x_size / 2, y_size / 2, z_size / 2);
        rb.mass = mass / 2;
        // "Delete" the minimizer from the inventory
        hasMinimizer = false;
        // Set the minimizer to active
        minimizerActive = true;
        // Start coroutine to time and cancel the pickup
        StartCoroutine("maxminTimer");
    }

    // Function that activates the maximizer-pickup
    private void activateMaximizer() {
        // Check if a minimizer-pickup is activated, if it has, stop the current coroutine
        if (minimizerActive == true) {
            StopCoroutine("maxminTimer");
        }
        // Apply the buff from the maximizer
        transform.localScale = new Vector3(x_size * 2, y_size * 2, z_size * 2);
        rb.mass = mass * 2;
        // "Delete" the maximizer from the inventory
        hasMaximizer = false;
        // Set the maximizer to active
        maximizerActive = true;
        // Start coroutine to time and cancel the pickup
        StartCoroutine("maxminTimer");

    }

    IEnumerator maxminTimer() {
        // Change the variable pickupDuration in order to change duration
        yield return new WaitForSeconds(pickupDuration);
        // Return to normal scale and mass
        transform.localScale = new Vector3(x_size, y_size, z_size);
        rb.mass = mass;
    }

    IEnumerator gravTimer() {
        yield return new WaitForSeconds(pickupDuration);
    }
}