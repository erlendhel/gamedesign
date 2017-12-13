using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class controlling all health related behaviour of the game character
public class PlayerHealth : MonoBehaviour {

    PlayerController playerController;
    public static PlayerHealth playerHealth;

    private float initialHealth = 20f;
    private float healthDecrease = 1.0f;
    public float currentHealth;

    private float smallIncrease = 10.0f;
    private float mediumIncrease = 15.0f;
    private float bigIncrease = 20.0f;

    // Panel used to signal the player that health is low
    public GameObject lowHealthWarning;
    private bool warningActive;

    // Fadeout when the player dies
    public GameObject fadeout;
    private Image fadingPanel;
    private float fadeTimer;
    private Color fadeColor;

    private Animator anim;
    private bool animationPlayable;

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

        fadingPanel = fadeout.gameObject.GetComponent<Image>();
        fadeColor = fadingPanel.color;
        fadeTimer = 0f;
        fadeout.SetActive(false);

        anim = GetComponent<Animator>();
        animationPlayable = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentHealth -= healthDecrease * Time.deltaTime;
        if (currentHealth <= 0)
        {
            fadeTimer += Time.deltaTime;
            RespawnAnimation();
            // When animation has lasted for >= 5 seconds
            if (fadeTimer >= 3f)
            {
                // Reset timer
                fadeTimer = 0f;

                // Reset color values and deactivate fade panel
                fadingPanel.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
                fadeColor = fadingPanel.color;
                fadeout.SetActive(false);

                // Reset players transform and refill health
                transform.position = playerController.GetSpawnPosition();
                currentHealth = initialHealth;
            }
        }
        else
        {
            // Check if health is below 20%, if so signal the player
            UpdateWarning();
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


    private void RespawnAnimation()
    {
        // Activate fadeout gameobject
        if (!fadeout.activeInHierarchy)
            fadeout.SetActive(true);

        fadingPanel.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeColor.a + Time.deltaTime / 5f);
        fadeColor = fadingPanel.color;
    }

    private void UpdateWarning()
    {

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
}
