using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    PlayerInventory playerInventory;
    PlayerController playerController;

    // Variables indicating if pickups have been activated
    private bool minimizerActive, maximizerActive, increaseGravActive, decreaseGravActive,
        timeBasedSpeedActive, timeBasedJumpActive = false;

    private Vector3 decreaseGrav = new Vector3(0, -4.5f, 0);
    private Vector3 increaseGrav = new Vector3(0, -20.0f, 0);

    //The power of the instant speed pickup
    private float instantSpeedPower = 5000f;

    private int pickupDuration = 5;

    private void Start() {
        playerInventory = GetComponent<PlayerInventory>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("1") && playerInventory.hasMinimizer == true) {
            ActivateMinimizer();
        } else if (Input.GetKeyDown("2") && playerInventory.hasMaximizer == true) {
            ActivateMaximizer();
        } else if (Input.GetKeyDown("3") && playerInventory.hasIncreaseGrav == true) {
            ActivateIncreaseGrav();
        } else if (Input.GetKeyDown("4") && playerInventory.hasDecreaseGrav == true) {
            ActivateDecreaseGrav();
        } else if (Input.GetKeyDown("5") && playerInventory.hasInstantJump == true) {
            ActivateInstantJump();
        } else if (Input.GetKeyDown("6") && playerInventory.hasTimeBasedJump == true) {
            ActivateTimeJump();
        } else if (Input.GetKeyDown("7") && playerInventory.hasInstantSpeed == true) {
            ActivateInstantSpeed();
        } else if (Input.GetKeyDown("8") && playerInventory.hasTimeBasedSpeed == true) {
            ActivateTimeSpeed();
        }
    }

    // Function that activates the minimizer-pickup
    public void ActivateMinimizer() {
        // Check if a maximizer-pickup is activated, if it has, stop the current coroutine
        if (maximizerActive == true) {
            StopCoroutine("MaxMinTimer");
        }
        // Apply the buff from the minimizer
        transform.localScale = playerController.initScale / 2;

        playerController.rb.mass = playerController.initMass / 2;

        // "Delete" the minimizer from the inventory
        playerInventory.hasMinimizer = false;
        // Set the minimizer to active
        minimizerActive = true;
        // Start coroutine to time and cancel the pickup
        StartCoroutine("MaxMinTimer");
    }

    // Function that activates the maximizer-pickup
    public void ActivateMaximizer() {

        // Check if a minimizer-pickup is activated, if it has, stop the current coroutine
        if (minimizerActive == true) {
            StopCoroutine("MaxMinTimer");
        }
        // Apply the buff from the maximizer
        transform.localScale = playerController.initScale * 2;

        playerController.rb.mass = playerController.initMass * 2;

        // "Delete" the maximizer from the inventory
        playerInventory.hasMaximizer = false;
        // Set the maximizer to active
        maximizerActive = true;
        // Start coroutine to time and cancel the pickup
        StartCoroutine("MaxMinTimer");
    }

    // Function that activates the increase-gravity-pickup
    public void ActivateIncreaseGrav() {
        // If decreaseGravity is active, stop the coroutine controlling gravity pickups
        if (decreaseGravActive == true) {
            StopCoroutine("GravTimer");
        }

        Physics.gravity = increaseGrav;

        playerInventory.hasIncreaseGrav = false;
        increaseGravActive = true;

        StartCoroutine("GravTimer");
    }

    public void ActivateDecreaseGrav() {
        if (increaseGravActive == true) {
            StopCoroutine("GravTimer");
        }
        Physics.gravity = decreaseGrav;

        playerInventory.hasDecreaseGrav = false;
        decreaseGravActive = true;
        StartCoroutine("GravTimer");
    }

    public void ActivateInstantSpeed() {
        //Set the movement to where the camera z-axis is pointing
        Vector3 movement = new Vector3(0, 0, 1);
        movement = Camera.main.transform.TransformDirection(movement);

        playerController.rb.AddForce(movement * instantSpeedPower);
        playerInventory.hasInstantSpeed = false;
    }

    public void ActivateTimeSpeed() {
        if (timeBasedSpeedActive == true) {
            StopCoroutine("SpeedTimer");
        }
        playerController.speed *= 2;

        playerInventory.hasTimeBasedSpeed = false;
        timeBasedSpeedActive = true;
        StartCoroutine("SpeedTimer");
    }

    //Activates Instant-jump pickup
    public void ActivateInstantJump() {
        if (playerController.isGrounded == true && 
                playerController.inWater == false) {

            playerController.rb.AddForce(Vector3.up * playerController.jumpSpeed * 3);

            playerInventory.hasInstantJump = false;
        } else {
            playerController.rb.AddForce(Vector3.up * playerController.jumpSpeed);
        }
    }

    public void ActivateTimeJump() {
        playerController.jumpSpeed = playerController.jumpSpeed * 2;
        playerInventory.hasTimeBasedJump = false;
        StartCoroutine("JumpTimer");
    }

    IEnumerator MaxMinTimer() {
        // Change the variable pickupDuration in order to change duration
        yield return new WaitForSeconds(pickupDuration);
        // Return to normal scale and mass
        transform.localScale = playerController.initScale;
        playerController.rb.mass = playerController.initMass;
    }

    IEnumerator GravTimer() {
        yield return new WaitForSeconds(pickupDuration);
        Physics.gravity = playerController.initGrav;
    }

    IEnumerator JumpTimer() {
        yield return new WaitForSeconds(pickupDuration);
        playerController.jumpSpeed = playerController.jumpSpeed / 2;
    }

    IEnumerator SpeedTimer() {
        yield return new WaitForSeconds(pickupDuration);
        playerController.speed = 15f;
    }
}
