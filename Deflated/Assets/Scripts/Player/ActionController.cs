using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

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
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("1") && PlayerInventory.hasMinimizer) {
            ActivateMinimizer();
            GUIController.guiController.minimizerButton.interactable = false;
        } else if (Input.GetKeyDown("2") && PlayerInventory.hasMaximizer) {
            ActivateMaximizer();
            GUIController.guiController.maximizerButton.interactable = false;
        } else if (Input.GetKeyDown("3") && PlayerInventory.hasDecreaseGrav) {
            ActivateDecreaseGrav();
            GUIController.guiController.decreaseGravButton.interactable = false;
        } else if (Input.GetKeyDown("4") && PlayerInventory.hasIncreaseGrav) {
            ActivateIncreaseGrav();
            GUIController.guiController.increaseGravButton.interactable = false;
        } else if (Input.GetKeyDown("5") && PlayerInventory.hasTimeBasedSpeed) {
            ActivateTimeSpeed();
            GUIController.guiController.timedSpeedButton.interactable = false;
        } else if (Input.GetKeyDown("6") && PlayerInventory.hasInstantSpeed) {
            ActivateInstantSpeed();
            GUIController.guiController.instantSpeedButton.interactable = false;
        } else if (Input.GetKeyDown("7") && PlayerInventory.hasTimeBasedJump) {
            ActivateTimeJump();
            GUIController.guiController.timedJumpButton.interactable = false;
        } else if (Input.GetKeyDown("8") && PlayerInventory.hasInstantJump) {
            ActivateInstantJump();
            GUIController.guiController.instantJumpButton.interactable = false;
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
        PlayerInventory.hasMinimizer = false;
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
        PlayerInventory.hasMaximizer = false;
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

        PlayerInventory.hasIncreaseGrav = false;
        increaseGravActive = true;

        StartCoroutine("GravTimer");
    }

    public void ActivateDecreaseGrav() {
        if (increaseGravActive == true) {
            StopCoroutine("GravTimer");
        }
        Physics.gravity = decreaseGrav;

        PlayerInventory.hasDecreaseGrav = false;
        decreaseGravActive = true;
        StartCoroutine("GravTimer");
    }

    public void ActivateInstantSpeed() {
        //Set the movement to where the camera z-axis is pointing
        Vector3 movement = new Vector3(0, 0, 1);
        movement = Camera.main.transform.TransformDirection(movement);

        playerController.rb.AddForce(movement * instantSpeedPower);
        PlayerInventory.hasInstantSpeed = false;
    }

    public void ActivateTimeSpeed() {
        if (timeBasedSpeedActive == true) {
            StopCoroutine("SpeedTimer");
        }
        playerController.speed *= 2;

        PlayerInventory.hasTimeBasedSpeed = false;
        timeBasedSpeedActive = true;
        StartCoroutine("SpeedTimer");
    }

    //Activates Instant-jump pickup
    public void ActivateInstantJump() {
        if (playerController.isGrounded == true && 
                playerController.inWater == false) {

            playerController.rb.AddForce(Vector3.up * playerController.jumpSpeed * 3);

            PlayerInventory.hasInstantJump = false;
        } else {
            playerController.rb.AddForce(Vector3.up * playerController.jumpSpeed);
        }
    }

    public void ActivateTimeJump() {
        playerController.jumpSpeed = playerController.jumpSpeed * 2;
        PlayerInventory.hasTimeBasedJump = false;
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
