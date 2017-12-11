using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class containing the functionality of the button combination puzzle. The basic functionality is that there is a predefined
 *  sequence which the user has to press the button to do something (open a door, initialize a lift etc.). The combination is 
 *  at the moment set to three variables, which are 'b' (blue), 'y' (yellow), and 'g' (green). When a player presses one of
 *  the buttons, the variable (either b, y, or g) will be appended to a string, and a boolean indicating whether or not 
 *  the given button has been pressed is set to true. This ensures that a color is only registered once per combination.
 *  To give the player a visual impression of pressing a button, there is a animation of the button being pressed in.
 *  For each loop of Update() the length of the 'combination' string is checked, if the length is 3, it means that all buttons
 *  are pressed, and the function will check if the sequence of chars in 'combination' is correct compared to the predefined
 *  combination. If not, the booleans and animations are reset, and the 'combination' string is cleared.
 **/
public class ButtonCombinationHandler : MonoBehaviour {
    // Game objects used in the button combination puzzle
    public GameObject yellowButton;
    public GameObject greenButton;
    public GameObject blueButton;
    public GameObject ironBarDoor;

    // Handlers for each button
    private ButtonHandler yellowHandler;
    private ButtonHandler greenHandler;
    private ButtonHandler blueHandler;

    // Animators for each game object in the combination puzzle
    private Animator yellowAnim;
    private Animator greenAnim;
    private Animator blueAnim;
    private Animator ironBarDoorAnim;

    // Booleans used to indicate if a color has been previously counted in the puzzle sequence
    private bool yellowCounted;
    private bool greenCounted;
    private bool blueCounted;

    private string preCombination = "byg";
    // The string which the characters get appended to in order to compare the predefined combination needed to solve the puzzle
    public string combination;

	void Start () {
        yellowButton.GetComponent<GameObject>();
        greenButton.GetComponent<GameObject>();
        blueButton.GetComponent<GameObject>();
        ironBarDoor.GetComponent<GameObject>();

        yellowHandler = yellowButton.GetComponent<ButtonHandler>();
        greenHandler = greenButton.GetComponent<ButtonHandler>();
        blueHandler = blueButton.GetComponent<ButtonHandler>();

        yellowAnim = yellowButton.GetComponent<Animator>();
        greenAnim = greenButton.GetComponent<Animator>();
        blueAnim = blueButton.GetComponent<Animator>();
        ironBarDoorAnim = ironBarDoor.GetComponent<Animator>();

        ironBarDoorAnim.enabled = false;
        yellowCounted = false;
        greenCounted = false;
        blueCounted = false;
        combination = "";
	}
	
	void Update () {
        // If sequence which checks if the different buttons are pressed through the respective button handlers, also
        // checks if a colors variable is already counted
        if (yellowHandler.isPressed == true && yellowCounted != true) {
            combination = combination + "y";
            yellowCounted = true;
        } else if (greenHandler.isPressed == true && greenCounted != true) {
            combination = combination + "g";
            greenCounted = true;
        } else if (blueHandler.isPressed == true && blueCounted != true) {
            blueCounted = true;
            combination = combination + "b";
        }

        // Check if the combination length is 3, if it is, it indicates that all the buttons have been pressed, and 
        // that the script needs to check if the right combination was pressed.
        if (combination.Length == 3) {
            // Check if the combination pressed by the user corresponds to what is given in the predefined combination
            if (combination == preCombination) {
                ironBarDoorAnim.enabled = true;
                print("Unlocked");
            // Reset the puzzle if the combination does not match
            } else {
                combination = "";
                yellowCounted = false;
                yellowHandler.isPressed = false;
                yellowAnim.enabled = false;
                greenCounted = false;
                greenHandler.isPressed = false;
                greenAnim.enabled = false;
                blueCounted = false;
                blueHandler.isPressed = false;
                blueAnim.enabled = false;
            }
        }
    }
}
