﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

    private bool isPressed = false;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

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
