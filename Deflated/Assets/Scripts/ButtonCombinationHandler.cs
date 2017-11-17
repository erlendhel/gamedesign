using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCombinationHandler : MonoBehaviour {
    public GameObject yellowButton;
    public GameObject greenButton;
    public GameObject blueButton;
    public GameObject ironBarDoor;

    private ButtonHandler yellowHandler;
    private ButtonHandler greenHandler;
    private ButtonHandler blueHandler;

    private Animator yellowAnim;
    private Animator greenAnim;
    private Animator blueAnim;
    private Animator ironBarDoorAnim;

    private bool yellowCounted;
    private bool greenCounted;
    private bool blueCounted;

    public string combination;

	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
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
        if (combination.Length == 3) {
            if (combination == "byg") {
                ironBarDoorAnim.enabled = true;
                print("Unlocked");
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
