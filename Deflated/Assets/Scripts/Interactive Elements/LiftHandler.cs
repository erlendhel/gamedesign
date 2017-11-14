using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftHandler : MonoBehaviour {

    private ButtonHandler buttonHandler;
    public GameObject button;
    private Animator anim;

	// Use this for initialization
	void Start () {
        buttonHandler = button.GetComponent<ButtonHandler>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (buttonHandler.getState()) {
            anim.enabled = true;
        }
	}
}
