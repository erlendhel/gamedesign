using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used to handle the behaviour of buttons found throughout the game. Controls the boolean variable 
 *  defining if a button is pressed or not. Also handles running of the animation. 
 **/
public class ButtonHandler : MonoBehaviour {

    public bool isPressed = false;
    public Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
      
    // Check for collision, if a there is a collision, play the animation and set the boolean to true.
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player") && !isPressed) {
            anim.enabled = true;
            isPressed = true;
        }
    }

    public bool getState() {
        return isPressed;
    }
}
