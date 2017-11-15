using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    private bool isPressed = false;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player") && !isPressed) {
            isPressed = true;
        }
    }

    public bool getState() {
        return isPressed;
    }
}
