using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class which handles the behaviour of a in-game lift. The lift is inactive until a button is pressed.
 *  When the button is pressed, the animation for the lift movement is enabled, thus providing the player
 *  with a way of transporting him/herself to higher ground.
 **/
public class LiftHandler : MonoBehaviour {

    // The button is handled by a button handler
    private ButtonHandler buttonHandler;
    public GameObject button;
    private Animator anim;

	void Start () {
        // Get the button handler attached to the button controlling the lift mechanism
        buttonHandler = button.GetComponent<ButtonHandler>();
        // Get the animator assigned to the lift object
        anim = GetComponent<Animator>();
        // Lift is not moving until the button is pressed
        anim.enabled = false;
	}
	
	void Update () {
        // Check the state of the button controlling the lift mechanism. If the state is returned
        // as pressed, the lift animation may start.
        if (buttonHandler.getState()) {
            anim.enabled = true;
        }
	}
}
