using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public static GUIController guiController;

    public Slider healthBarSlider;
    public Button maximizerButton, minimizerButton, decreaseGravButton,
        increaseGravButton, timedSpeedButton, instantSpeedButton, timedJumpButton,
            instantJumpButton;

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
    }

    // Update is called once per frame
    void Update () {
        healthBarSlider.value = PlayerHealth.playerHealth.currentHealth;
	}
}
