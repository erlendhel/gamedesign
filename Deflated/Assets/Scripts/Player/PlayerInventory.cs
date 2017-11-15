using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour {

    // Variables indicating if the player has a pickup or not. Public because they are written to in ActionController
    public static bool hasMinimizer, hasMaximizer, hasDecreaseGrav, hasIncreaseGrav,
        hasInstantSpeed, hasTimeBasedSpeed, hasInstantJump, hasTimeBasedJump = false;

    // Variable indicating if the player has a key (used in scenarios where the player picks up the whole key)
    private static bool hasKey = false;
    
    // Variables indicating which keyparts the player has picked up
    private static bool hasFirstKeyPart, hasSecondKeyPart, hasThirdKeyPart = false;


    private void OnTriggerEnter(Collider pickup) {
        if (pickup.gameObject.CompareTag("Minimizer"))
        {
            hasMinimizer = true;
            GUIController.guiController.minimizerButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("Maximizer"))
        {
            hasMaximizer = true;
            GUIController.guiController.maximizerButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("IncreaseGrav"))
        {
            hasIncreaseGrav = true;
            GUIController.guiController.increaseGravButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("DecreaseGrav"))
        {
            hasDecreaseGrav = true;
            GUIController.guiController.decreaseGravButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("InstantJump"))
        {
            hasInstantJump = true;
            GUIController.guiController.instantJumpButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("TimeBasedJump"))
        {
            hasTimeBasedJump = true;
            GUIController.guiController.timedJumpButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("SpeedInstant"))
        {
            hasInstantSpeed = true;
            GUIController.guiController.instantSpeedButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("SpeedTime"))
        {
            hasTimeBasedSpeed = true;
            GUIController.guiController.timedSpeedButton.interactable = true;
        }
        else if (pickup.gameObject.CompareTag("Key"))
        {
            hasKey = true;
        }
        else if (pickup.gameObject.CompareTag("FirstKeyPart"))
        {
            hasFirstKeyPart = true;
            GUIController.guiController.SetKeyImage();
        }
        else if (pickup.gameObject.CompareTag("SecondKeyPart"))
        {
            hasSecondKeyPart = true;
            GUIController.guiController.SetKeyImage();
        }
        else if (pickup.gameObject.CompareTag("ThirdKeyPart"))
        {
            hasThirdKeyPart = true;
            GUIController.guiController.SetKeyImage();
        }
    }

    // Used to check on collisions with locks that require one whole key to be picked up
    public static bool HasKey()
    {
        return hasKey;
    }

    // Used to check on collison with locks that require all parts of a key to be picked up
    public static bool HasAllKeyParts()
    {
        return hasFirstKeyPart && hasSecondKeyPart && hasThirdKeyPart;
    }

    public static bool HasBottomKeyPart()
    {
        return hasFirstKeyPart && !hasSecondKeyPart && !hasThirdKeyPart;
    }

    public static bool HasMiddleKeyPart()
    {
        return !hasFirstKeyPart && hasSecondKeyPart && !hasThirdKeyPart;
    }

    public static bool HasTopKeyPart()
    {
        return !hasFirstKeyPart && !hasSecondKeyPart && hasThirdKeyPart;
    }

    public static bool HasBottomAndMiddleKeyPart()
    {
        return hasFirstKeyPart && hasSecondKeyPart && !hasThirdKeyPart;
    }

    public static bool HasMiddleAndTopKeyPart()
    {
        return !hasFirstKeyPart && hasSecondKeyPart && hasThirdKeyPart;
    }

    public static bool HasTopAndBottomKeyPart()
    {
        return hasFirstKeyPart && !hasSecondKeyPart && hasThirdKeyPart;
    }
}
