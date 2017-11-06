using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    // Variables indicating if the player has a pickup or not
    public static bool hasMinimizer, hasMaximizer, hasDecreaseGrav, hasIncreaseGrav,
        hasInstantSpeed, hasTimeBasedSpeed, hasInstantJump, hasTimeBasedJump,
            hasKey = false;

    private void OnTriggerEnter(Collider pickup) {
        if (pickup.gameObject.CompareTag("Minimizer")) {
            hasMinimizer = true;
            GUIController.guiController.minimizerButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("Maximizer")) {
            hasMaximizer = true;
            GUIController.guiController.maximizerButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("IncreaseGrav")) {
            hasIncreaseGrav = true;
            GUIController.guiController.increaseGravButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("DecreaseGrav")) {
            hasDecreaseGrav = true;
            GUIController.guiController.decreaseGravButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("InstantJump")) {
            hasInstantJump = true;
            GUIController.guiController.instantJumpButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("TimeBasedJump")) {
            hasTimeBasedJump = true;
            GUIController.guiController.timedJumpButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("SpeedInstant")) {
            hasInstantSpeed = true;
            GUIController.guiController.instantSpeedButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("SpeedTime")) {
            hasTimeBasedSpeed = true;
            GUIController.guiController.timedSpeedButton.interactable = true;
        } else if (pickup.gameObject.CompareTag("Key")) {
            hasKey = true;
        }
    }
}
