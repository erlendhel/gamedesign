using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoJumpHandler : MonoBehaviour {

    PlayerController playerController;

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            print("Entered no jump zone");
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.canJump = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            playerController.canJump = true;
        }
    }

}
