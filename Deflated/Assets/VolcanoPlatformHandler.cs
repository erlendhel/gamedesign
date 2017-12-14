using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoPlatformHandler : MonoBehaviour {

    Animator anim;

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.canJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (name.Equals("LastPlatform")) {

            if (anim == null) {
                anim = GetComponent<Animator>();
            }

            if (collision.gameObject.CompareTag("Player")) {
                anim.Play("ActivateLift");
            }
        }
    }
}

