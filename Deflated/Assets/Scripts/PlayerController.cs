using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 15f;
    public float jumpSpeed = 500.0f;
    private bool isGrounded = true;
    private bool inWater = false;
    private Rigidbody rb;

    public float vel;



    // Default values of game character, NOT directly linked with the ones in Unity.
    private float x_size = 50.0f;
    private float y_size = 50.0f;
    private float z_size = 50.0f;
    private float mass = 1.0f;

    private Vector3 initGrav = new Vector3(0, -9.81f, 0);
    private Vector3 decreaseGrav = new Vector3(0, -4.5f, 0);
    private Vector3 increaseGrav = new Vector3(0, -20.0f, 0);

    
    // Variables used to determine states of pickups
    public bool hasMinimizer = false;
    public bool hasMaximizer = false;
    public bool hasIncreaseGrav = false;
    public bool hasDecreaseGrav = false;
    public bool hasInstantSpeed = false;
    public bool hasTimeSpeed = false;
    public bool hasInstantJump = false;
    public bool hasTimeJump = false;

    //To determine if a pickup is currently in use by the player
    private bool minimizerActive = false;
    private bool maximizerActive = false;
    private bool increaseGravActive = false;
    private bool decreaseGravActive = false;
    private bool timeSpeedActive = false;

    private int pickupDuration = 5;

    public bool hasKey = false;

    //The power of the instant speed pickup
    private float instantSpeedPower = 5000f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = initGrav;
    }

    private void Update() {

        vel = rb.velocity.magnitude;

        if (Input.GetKeyDown("space") && isGrounded == true && inWater == false) {
            Jump();
        } else if (Input.GetKeyDown("1") && hasMinimizer == true) {
            ActivateMinimizer();
        } else if (Input.GetKeyDown("2") && hasMaximizer == true) {
            ActivateMaximizer();
        } else if (Input.GetKeyDown("3") && hasIncreaseGrav == true) {
            ActivateIncreaseGrav();
        } else if (Input.GetKeyDown("4") && hasDecreaseGrav == true) {
            ActivateDecreaseGrav();
        } else if (Input.GetKeyDown("5") && hasInstantJump == true){
            ActivateInstantJump();
        } else if (Input.GetKeyDown("6") && hasTimeJump == true){
            ActivateTimeJump();
        }else if(Input.GetKeyDown("7") && hasInstantSpeed == true) {
            ActivateInstantSpeed();
        }else if (Input.GetKeyDown("8") && hasTimeSpeed == true){
            ActivateTimeSpeed();
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
    
    // Function to detect if the game character goes over a pickup.
    private void OnTriggerEnter(Collider other) {
        // Checking if the player collides with a minimizer pickup
        if (other.gameObject.CompareTag("Minimizer")) {
            hasMinimizer = true; // Player has acquired a minimizer pickup
        } else if (other.gameObject.CompareTag("Maximizer")) {
            hasMaximizer = true; // Player has acquired a maximizer pickup
        } else if (other.gameObject.CompareTag("IncreaseGrav")) {
            hasIncreaseGrav = true;
        } else if (other.gameObject.CompareTag("DecreaseGrav")) {
            hasDecreaseGrav = true;
        } else if (other.gameObject.CompareTag("Ijump")) {
            hasInstantJump = true;
        } else if (other.gameObject.CompareTag("Tbjump")) {
            hasTimeJump = true;
        } else if (other.gameObject.CompareTag("SpeedInstant")) {
            hasInstantSpeed = true;
        } else if (other.gameObject.CompareTag("SpeedTime")) {
            hasTimeSpeed = true;
        }

        if (other.gameObject.CompareTag("Key")){
            hasKey = true;
        }

    }

    // Function that activates the minimizer-pickup
    private void ActivateMinimizer() {
        // Check if a maximizer-pickup is activated, if it has, stop the current coroutine
        if (maximizerActive == true) {
            StopCoroutine("MaxMinTimer");
        }
        // Apply the buff from the minimizer
        transform.localScale = new Vector3(x_size / 2, y_size / 2, z_size / 2);
        rb.mass = mass / 2;
        // "Delete" the minimizer from the inventory
        hasMinimizer = false;
        // Set the minimizer to active
        minimizerActive = true;
        // Start coroutine to time and cancel the pickup
        StartCoroutine("MaxMinTimer");
    }

    // Function that activates the maximizer-pickup
    private void ActivateMaximizer() {
        // Check if a minimizer-pickup is activated, if it has, stop the current coroutine
        if (minimizerActive == true) {
            StopCoroutine("MaxMinTimer");
        }
        // Apply the buff from the maximizer
        transform.localScale = new Vector3(x_size * 2, y_size * 2, z_size * 2);
        rb.mass = mass * 2;
        // "Delete" the maximizer from the inventory
        hasMaximizer = false;
        // Set the maximizer to active
        maximizerActive = true;
        // Start coroutine to time and cancel the pickup
        StartCoroutine("MaxMinTimer");

    }

    // Function that activates the increase-gravity-pickup
    private void ActivateIncreaseGrav() {
        // If decreaseGravity is active, stop the coroutine controlling gravity pickups
        if (decreaseGravActive == true) {
            StopCoroutine("GravTimer");
        }

        Physics.gravity = increaseGrav;

        hasIncreaseGrav = false;
        increaseGravActive = true;

        StartCoroutine("GravTimer");
    }

    private void ActivateDecreaseGrav() {
        if (increaseGravActive == true) {
            StopCoroutine("GravTimer");
        }
        Physics.gravity = decreaseGrav;

        hasDecreaseGrav = false;
        decreaseGravActive = true;
        StartCoroutine("GravTimer");
    }

    private void ActivateInstantSpeed() { 
        //Set the movement to where the camera z-axis is pointing
        Vector3 movement = new Vector3(0,0,1);
        movement = Camera.main.transform.TransformDirection(movement);
        
        rb.AddForce(movement * instantSpeedPower);
        hasInstantSpeed = false;
    }

    private void ActivateTimeSpeed() {
        if (timeSpeedActive == true) {
            StopCoroutine("SpeedTimer");
        }
        speed *= 2;

        hasTimeSpeed = false;
        timeSpeedActive = true;
        StartCoroutine("SpeedTimer");
    }

    //Activates Instant-jump pickup
    private void ActivateInstantJump() {
        if (isGrounded == true && inWater == false) {

            rb.AddForce(Vector3.up * jumpSpeed * 3);

            hasInstantJump = false;
        }

        else{
            rb.AddForce(Vector3.up * jumpSpeed);
        }
    }

    private void ActivateTimeJump() {
        jumpSpeed = jumpSpeed * 2;
      
        hasTimeJump = false;
        StartCoroutine("JumpTimer");
    }

    IEnumerator MaxMinTimer() {
        // Change the variable pickupDuration in order to change duration
        yield return new WaitForSeconds(pickupDuration);
        // Return to normal scale and mass
        transform.localScale = new Vector3(x_size, y_size, z_size);
        rb.mass = mass;
    }

    IEnumerator GravTimer() {
        yield return new WaitForSeconds(pickupDuration);
        Physics.gravity = initGrav;
    }

    IEnumerator JumpTimer(){
        yield return new WaitForSeconds(pickupDuration);
        jumpSpeed =  jumpSpeed / 2 ;
    }
  
    IEnumerator SpeedTimer(){
        yield return new WaitForSeconds(pickupDuration);
        speed = 15f;

    }
}