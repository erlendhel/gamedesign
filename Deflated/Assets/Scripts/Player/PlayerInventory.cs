using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class controlling the inventory of a player in 'play-mode'.
 **/
public class PlayerInventory : MonoBehaviour {

    // Booleans indicating if a player has a pickup or not.
    public static bool hasMinimizer, hasMaximizer, hasDecreaseGrav, hasIncreaseGrav,
        hasInstantSpeed, hasTimeBasedSpeed, hasInstantJump, hasTimeBasedJump = false;

    // Variable indicating if the player has a key (used in scenarios where the player picks up the whole key)
    private static bool hasKey = false;
    
    // Variables indicating which keyparts the player has picked up
    private static bool hasFirstKeyPart, hasSecondKeyPart, hasThirdKeyPart = false;

    private void OnTriggerEnter(Collider pickup) {
        if (pickup.gameObject.CompareTag("Minimizer")) {
            hasMinimizer = true;
            GUIController.guiController.minimizerButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.minimizerImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.minimizerImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("Maximizer")) {
            hasMaximizer = true;
            GUIController.guiController.maximizerButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.maximizerImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.maximizerImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("IncreaseGrav")) {
            hasIncreaseGrav = true;
            GUIController.guiController.increaseGravButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.increaseGravImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.increaseGravImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("DecreaseGrav")) {
            hasDecreaseGrav = true;
            GUIController.guiController.decreaseGravButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.decreaseGravImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.decreaseGravImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("InstantJump")) {
            hasInstantJump = true;
            GUIController.guiController.instantJumpButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.instantJumpImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.instantJumpImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("TimeBasedJump")) {
            hasTimeBasedJump = true;
            GUIController.guiController.timedJumpButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.timeJumpImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.timeJumpImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("SpeedInstant")) {
            hasInstantSpeed = true;
            GUIController.guiController.instantSpeedButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.instantSpeedImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.instantSpeedImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("SpeedTime")) {
            hasTimeBasedSpeed = true;
            GUIController.guiController.timedSpeedButton.interactable = true;

            // Set image color to indicate it is in inventory
            Color oldImageColor = GUIController.guiController.timeSpeedImage.color;
            Color newImageColor = new Color(oldImageColor.r, oldImageColor.g, oldImageColor.b, 1f);
            GUIController.guiController.timeSpeedImage.color = newImageColor;
        } else if (pickup.gameObject.CompareTag("Key")) {
            hasKey = true;
        } else if (pickup.gameObject.CompareTag("FirstKeyPart")) {
            hasFirstKeyPart = true;
            GUIController.guiController.SetKeyImage();
        } else if (pickup.gameObject.CompareTag("SecondKeyPart")) {
            hasSecondKeyPart = true;
            GUIController.guiController.SetKeyImage();
        } else if (pickup.gameObject.CompareTag("ThirdKeyPart")) {
            hasThirdKeyPart = true;
            GUIController.guiController.SetKeyImage();
        }
    }

    // Used to check on collisions with locks that require one whole key to be picked up
    public static bool HasKey() {
        return hasKey;
    }

    // Used to check on collison with locks that require all parts of a key to be picked up
    public static bool HasAllKeyParts() {
        return hasFirstKeyPart && hasSecondKeyPart && hasThirdKeyPart;
    }

    public static bool HasBottomKeyPart() {
        return hasFirstKeyPart && !hasSecondKeyPart && !hasThirdKeyPart;
    }

    public static bool HasMiddleKeyPart() {
        return !hasFirstKeyPart && hasSecondKeyPart && !hasThirdKeyPart;
    }

    public static bool HasTopKeyPart() {
        return !hasFirstKeyPart && !hasSecondKeyPart && hasThirdKeyPart;
    }

    public static bool HasBottomAndMiddleKeyPart() {
        return hasFirstKeyPart && hasSecondKeyPart && !hasThirdKeyPart;
    }

    public static bool HasMiddleAndTopKeyPart() {
        return !hasFirstKeyPart && hasSecondKeyPart && hasThirdKeyPart;
    }

    public static bool HasTopAndBottomKeyPart() {
        return hasFirstKeyPart && !hasSecondKeyPart && hasThirdKeyPart;
    }
}
