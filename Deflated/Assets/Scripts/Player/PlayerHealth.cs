using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class controlling all health related behaviour of the game character
public class PlayerHealth : MonoBehaviour {

    PlayerController playerController;
    public static PlayerHealth playerHealth;

    private float initialHealth = 100f;
    private float healthDecrease = 1.0f;
    public float currentHealth;

    private float smallIncrease = 10.0f;
    private float mediumIncrease = 15.0f;
    private float bigIncrease = 20.0f;

    public GameObject lowHealthWarning;
    private bool warningActive;

    // Used to time the flashing of the warning panel
    private float timer;
    private bool timerActive;
    private float timerLimit;

    // Use this for initialization
    private void Awake() {
        if (playerHealth == null) {
            playerHealth = this;
        }
    }

    void Start () {
        playerController = GetComponent<PlayerController>();
        currentHealth = initialHealth;

        // Related to warning panel
        warningActive = lowHealthWarning.activeInHierarchy;
        timer = 0f;
        timerActive = false;
        timerLimit = 0.20f;
	}
	
	// Update is called once per frame
	void Update () {
        currentHealth -= healthDecrease * Time.deltaTime;
        if (currentHealth < 0) {
            transform.position = playerController.GetSpawnPosition();
            currentHealth = 100.0f;
        }

        // Timer used to activate and deactivate warning panel for low health 
        if (timerActive)
            timer += Time.deltaTime;

        // When health goes from >20 to <=20
        if(currentHealth <= 20 && !timerActive)
        {
            lowHealthWarning.SetActive(!warningActive);
            timerActive = true;
            warningActive = !warningActive;
        }
        // If Health is below 20% and the timer has exceeded timeLimit variable
        else if(currentHealth <= 20f && timer >= timerLimit)
        {
            // Reset timer and change active state of warning
            timer = 0f;
            lowHealthWarning.SetActive(!warningActive);
            warningActive = !warningActive;

            // Make sure warning panel doesn't blink to fast when health is below 10%
            if (currentHealth >= 10f)
                timerLimit = currentHealth / 100f;
            else
                timerLimit = 0.10f;
        }
        // If health goes over 20% after being below 20%
        else if(currentHealth >= 20f && timerActive)
        {
            // Disable warning and reset timer
            lowHealthWarning.SetActive(false);
            timerActive = false;
            timer = 0f;
        }
	}

    private void OnTriggerEnter(Collider bubble) {
        if (bubble.gameObject.CompareTag("SmallBubble")) {
            CurrencyManager.currencyManager.currency += smallIncrease;
            CurrencyManager.currencyManager.Save();
            if ((currentHealth + smallIncrease) >= 100.0f) {
                currentHealth = 100.0f;
            } else {
                currentHealth += smallIncrease;
            }
        } else if (bubble.gameObject.CompareTag("MediumBubble")) {
            CurrencyManager.currencyManager.currency += mediumIncrease;
            CurrencyManager.currencyManager.Save();
            if ((currentHealth + mediumIncrease) >= 100.0f) {
                currentHealth = 100.0f;
            } else {
                currentHealth += mediumIncrease;
            }
        } else if (bubble.gameObject.CompareTag("LargeBubble")) {
            CurrencyManager.currencyManager.currency += bigIncrease;
            CurrencyManager.currencyManager.Save();
            if ((currentHealth + bigIncrease) >= 100.0f) {
                currentHealth = 100.0f;
            } else {
                currentHealth += bigIncrease;
            }
        }
    }
}
