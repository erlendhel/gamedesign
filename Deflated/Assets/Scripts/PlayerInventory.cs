using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    // Variables indicating if the player has a pickup or not
    public bool hasMinimizer, hasMaximizer, hasDecreaseGrav, hasIncreaseGrav,
        hasInstantSpeed, hasTimeBasedSpeed, hasInstantJump, hasTimeBasedJump,
            hasKey = false;

    private void OnTriggerEnter(Collider pickup) {
        if (pickup.gameObject.CompareTag("Minimizer")) {
            hasMinimizer = true;
        } else if (pickup.gameObject.CompareTag("Maximizer")) {
            hasMaximizer = true;
        } else if (pickup.gameObject.CompareTag("IncreaseGrav")) {
            hasIncreaseGrav = true;
        } else if (pickup.gameObject.CompareTag("DecreaseGrav")) {
            hasDecreaseGrav = true;
        } else if (pickup.gameObject.CompareTag("InstantJump")) {
            hasInstantJump = true;
        } else if (pickup.gameObject.CompareTag("TimeBasedJump")) {
            hasTimeBasedJump = true;
        } else if (pickup.gameObject.CompareTag("SpeedInstant")) {
            hasInstantSpeed = true;
        } else if (pickup.gameObject.CompareTag("SpeedTime")) {
            hasTimeBasedSpeed = true;
        } else if (pickup.gameObject.CompareTag("Key")) {
            hasKey = true;
        }
    }
}
