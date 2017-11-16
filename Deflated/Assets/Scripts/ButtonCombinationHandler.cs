using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCombinationHandler : MonoBehaviour {

    public GameObject ironBarDoor;
    public GameObject yellowButton;
    public GameObject greenButton;
    public GameObject blueButton;
    private Animator yellowAnim;
    private Animator greenAnim;
    private Animator blueAnim;

    private bool yellowPressed;
    private bool greenPressed;
    private bool bluePressed;

	// Use this for initialization
	void Start () {
        ironBarDoor = GetComponent<GameObject>();
        yellowButton = GetComponent<GameObject>();
        greenButton = GetComponent<GameObject>();
        blueButton = GetComponent<GameObject>();
        yellowAnim = yellowButton.GetComponent<Animator>();
        greenAnim = greenButton.GetComponent<Animator>();
        blueAnim = blueButton.GetComponent<Animator>();

        yellowAnim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
