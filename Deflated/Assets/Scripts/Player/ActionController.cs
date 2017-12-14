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
    private float instantSpeedPower = 3000f;

    private int pickupDuration = 5;

    private void Start() {
        playerController = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("1") && PlayerInventory.hasMinimizer) {
            ActivateMinimizer();
        } else if (Input.GetKeyDown("2") && PlayerInventory.hasMaximizer) {
            ActivateMaximizer();
        } else if (Input.GetKeyDown("3") && PlayerInventory.hasDecreaseGrav) {
            ActivateDecreaseGrav();
        } else if (Input.GetKeyDown("4") && PlayerInventory.hasIncreaseGrav) {
            ActivateIncreaseGrav();
        } else if (Input.GetKeyDown("5") && PlayerInventory.hasTimeBasedSpeed) {
            ActivateTimeSpeed();
        } else if (Input.GetKeyDown("6") && PlayerInventory.hasInstantSpeed) {
            ActivateInstantSpeed();
        } else if (Input.GetKeyDown("7") && PlayerInventory.hasTimeBasedJump) {
            ActivateTimeJump();
        } else if (Input.GetKeyDown("8") && PlayerInventory.hasInstantJump) {
            ActivateInstantJump();
        }
       
    }

    // Function that activates the minimizer-pickup
    public void ActivateMinimizer() {
        // Set image color to indicate it no longer in inventory
        Color oldImageColor = GUIController.guiController.minimizerImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.minimizerImage.color = newImageColor;

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
        GUIController.guiController.minimizerButton.interactable = false;
    }

    // Function that activates the maximizer-pickup
    public void ActivateMaximizer() {
        // Set image color to indicate it no longer in inventory
        Color oldImageColor = GUIController.guiController.maximizerImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.maximizerImage.color = newImageColor;

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
        GUIController.guiController.maximizerButton.interactable = false;
    }

    // Function that activates the increase-gravity-pickup
    public void ActivateIncreaseGrav() {
        // Set image color to indicate it no longer in inventory
        Color oldImageColor = GUIController.guiController.increaseGravImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.increaseGravImage.color = newImageColor;

        // If decreaseGravity is active, stop the coroutine controlling gravity pickups
        if (decreaseGravActive == true) {
            StopCoroutine("GravTimer");
        }

        Physics.gravity = increaseGrav;

        PlayerInventory.hasIncreaseGrav = false;
        increaseGravActive = true;

        StartCoroutine("GravTimer");
        GUIController.guiController.increaseGravButton.interactable = false;
    }

    public void ActivateDecreaseGrav() {
        // Set image color to indicate it is in inventory
        Color oldImageColor = GUIController.guiController.decreaseGravImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.decreaseGravImage.color = newImageColor;

        if (increaseGravActive == true) {
            StopCoroutine("GravTimer");
        }
        Physics.gravity = decreaseGrav;

        PlayerInventory.hasDecreaseGrav = false;
        decreaseGravActive = true;
        StartCoroutine("GravTimer");
        GUIController.guiController.decreaseGravButton.interactable = false;
    }

    public void ActivateInstantSpeed() {
        // Set image color to indicate it is in inventory
        Color oldImageColor = GUIController.guiController.instantSpeedImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.instantSpeedImage.color = newImageColor;

        //Set the movement to where the camera z-axis is pointing
        Vector3 movement = new Vector3(0, 0, 1);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movement = Vector3.Normalize(movement);

        playerController.rb.AddForce(movement * instantSpeedPower);
        PlayerInventory.hasInstantSpeed = false;
        GUIController.guiController.instantSpeedButton.interactable = false;
    }

    public void ActivateTimeSpeed() {
        // Set image color to indicate it no longer in inventory
        Color oldImageColor = GUIController.guiController.timeSpeedImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.timeSpeedImage.color = newImageColor;

        if (timeBasedSpeedActive == true) {
            StopCoroutine("SpeedTimer");
        }
        playerController.speed *= 2;
        Debug.Log("Time Speed");

        PlayerInventory.hasTimeBasedSpeed = false;
        timeBasedSpeedActive = true;
        StartCoroutine("SpeedTimer");
        GUIController.guiController.timedSpeedButton.interactable = false;
    }

    //Activates Instant-jump pickup
    public void ActivateInstantJump() {
        // Set image color to indicate it is in inventory
        Color oldImageColor = GUIController.guiController.instantJumpImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.instantJumpImage.color = newImageColor;

        if (playerController.isGrounded == true && 
                playerController.inWater == false) {

            playerController.rb.AddForce(Vector3.up * playerController.jumpSpeed * 3);

            PlayerInventory.hasInstantJump = false;
        } else {
            playerController.rb.AddForce(Vector3.up * playerController.jumpSpeed);
        }

        GUIController.guiController.instantJumpButton.interactable = false;
    }

    public void ActivateTimeJump() {
        // Set image color to indicate it no longer in inventory
        Color oldImageColor = GUIController.guiController.timeJumpImage.color;
        Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 0.1f);
        GUIController.guiController.timeJumpImage.color = newImageColor;
        
        playerController.jumpSpeed = playerController.jumpSpeed * 2;
        PlayerInventory.hasTimeBasedJump = false;
        StartCoroutine("JumpTimer");
        GUIController.guiController.timedJumpButton.interactable = false;
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
