using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GUIController : MonoBehaviour {

    public static GUIController guiController;

    public Slider healthBarSlider;
    public Button maximizerButton, minimizerButton, decreaseGravButton,
        increaseGravButton, timedSpeedButton, instantSpeedButton, timedJumpButton,
            instantJumpButton;

    // All the sprites used to show different combinations of which key parts are picked up
    public KeySprite[] keyImages = new KeySprite[8];
    public Image keyImage;
    private uint keyPartsCollected = 0;
    private Color keyImageColor;
    private float startAlphaValue;


    private void Start() {
        if (guiController == null) {
            guiController = this;
        }
        maximizerButton.interactable = false;
        minimizerButton.interactable = false;
        decreaseGravButton.interactable = false;
        increaseGravButton.interactable = false;
        timedSpeedButton.interactable = false;
        instantSpeedButton.interactable = false;
        timedJumpButton.interactable = false;
        instantJumpButton.interactable = false;

        keyImageColor = keyImage.color;
        startAlphaValue = keyImage.color.a;

    }

    // Update is called once per frame
    void Update ()
    {
        healthBarSlider.value = PlayerHealth.playerHealth.currentHealth;
    }

    private void SetKeyImage(string name)
    {
        KeySprite keySprite = Array.Find(keyImages, keysprite => keysprite.name == name);

        if (keySprite == null)
        {
            Debug.Log("Could not find sprite with name: " + name);
            return;
        }

        keyImage.sprite = keySprite.image;
    }

    public void SetKeyImage()
    {
        // Keep track of how many key parts the player has collected. Used to set alpha color value of sprite
        ++keyPartsCollected;

        if (PlayerInventory.HasBottomKeyPart())
            SetKeyImage("Bottom");
        else if (PlayerInventory.HasMiddleKeyPart())
            SetKeyImage("Middle");
        else if (PlayerInventory.HasTopKeyPart())
            SetKeyImage("Top");
        else if (PlayerInventory.HasBottomAndMiddleKeyPart())
            SetKeyImage("BottomMiddle");
        else if (PlayerInventory.HasMiddleAndTopKeyPart())
            SetKeyImage("MiddleTop");
        else if (PlayerInventory.HasTopAndBottomKeyPart())
            SetKeyImage("BottomTop");
        else if (PlayerInventory.HasAllKeyParts())
            SetKeyImage("Filled");

        // Gradually increase alpha value of sprite color for each part being picked up
        keyImage.color = new Color(keyImageColor.r,keyImageColor.g, keyImageColor.b, startAlphaValue + keyPartsCollected / 5f);
    }
}
