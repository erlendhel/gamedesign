using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class controlling all health related behaviour of the game character
public class PlayerHealth : MonoBehaviour {

    PlayerController playerController;
    public static PlayerHealth playerHealth;

    private float initialHealth = 100f;
    private float healthDecrease = 1.2f;
    public float currentHealth;

    // Health and currency values of the three different air bubble sizes
    private float smallIncrease = 10.0f;
    private float mediumIncrease = 15.0f;
    private float bigIncrease = 20.0f;

    public bool inLava = false;

    // To disable the player from increasing health from airbubbles when respawning text has appeared
    private bool respawning = false;

    // Frame used to signal the player that health is low
    public GameObject lowHealthWarning;
    private bool warningActive;

    // Fadeout when the player dies
    public GameObject fadeout;
    private Image fadingPanel;
    private float fadeTimer;
    private Color fadeColor;

    // Currently not in use, seem to be a problem with playing the animation
    private Animator anim;
    private bool animationCanBePlayed;

    public GameObject respawningText;

    // Used to time the flashing of the warning frame
    private float timer;
    private bool timerActive;
    private float timerLimit;

    // Use this for initialization
    private void Awake() {
        if (playerHealth == null) {
            playerHealth = this;
        }
    }

    void Start ()
    {
        playerController = GetComponent<PlayerController>();
        currentHealth = initialHealth;

        // Related to flashing warning frame
        warningActive = lowHealthWarning.activeInHierarchy;
        timer = 0f;
        timerActive = false;
        timerLimit = 0.20f;

        // A gray transparent panel which gradually fade in when player health reaches 0%
        fadingPanel = fadeout.gameObject.GetComponent<Image>();
        fadeColor = fadingPanel.color;
        fadeTimer = 0f;
        fadeout.SetActive(false);

        anim = GetComponent<Animator>();
        animationCanBePlayed = true;
	}
	
	void Update ()
    {
        // Decrease health by small amount every frame update
        currentHealth -= healthDecrease * Time.deltaTime;

        if (playerController.inWater) {
            StartCoroutine("WaterDamage");
        }
        if (inLava) {
            StartCoroutine("LavaDamage");
        }

        // When player runs out of health
        if (currentHealth <= 0)
            RespawnAnimation();
        // Check if health is below 20%, if so signal the player
        else
            UpdateFlashingFrame();
	}

    /**
     * Increase health and currency on collision with air bubbles
     * **/
    private void OnTriggerEnter(Collider bubble) {
        if (bubble.gameObject.CompareTag("SmallBubble") && !respawning) {
            CurrencyManager.currencyManager.currency += smallIncrease;
            CurrencyManager.currencyManager.Save();
            if ((currentHealth + smallIncrease) >= 100.0f) {
                currentHealth = 100.0f;
            } else {
                currentHealth += smallIncrease;
            }
        } else if (bubble.gameObject.CompareTag("MediumBubble")&& !respawning) {
            CurrencyManager.currencyManager.currency += mediumIncrease;
            CurrencyManager.currencyManager.Save();
            if ((currentHealth + mediumIncrease) >= 100.0f) {
                currentHealth = 100.0f;
            } else {
                currentHealth += mediumIncrease;
            }
        } else if (bubble.gameObject.CompareTag("LargeBubble")&& !respawning) {
            CurrencyManager.currencyManager.currency += bigIncrease;
            CurrencyManager.currencyManager.Save();
            if ((currentHealth + bigIncrease) >= 100.0f) {
                currentHealth = 100.0f;
            } else {
                currentHealth += bigIncrease;
            }
        }
    }

    /**
     * Function which over 4 seconds fades a gray transparent panel indicating the player
     * has died. Also activates a text singaling the player that he/she is respawning
     **/
    private void RespawnAnimation()
    {
        // Increase timer
        fadeTimer += Time.deltaTime;
        respawning = true;

        // Activate fadeout gameobject
        if (!fadeout.activeInHierarchy)
            fadeout.SetActive(true);

        // Activate "respawning" text
        if (!respawningText.activeInHierarchy)
            respawningText.SetActive(true);

        // Remove red frame if it is active
        if (lowHealthWarning.activeInHierarchy)
            lowHealthWarning.SetActive(false);

        // Start playing animation as soon as health reaches 0
        if (animationCanBePlayed)
        {
            anim.Play("Death");
            animationCanBePlayed = false;
        }

        // Gradually increase the alpha value of the fadeout panel
        fadingPanel.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeColor.a + Time.deltaTime / 5f);
        fadeColor.a = fadingPanel.color.a;

        // When animation has lasted for >= 5 seconds
        if (fadeTimer >= 4f)
        {
            // Reset timer
            fadeTimer = 0f;

            // Reset color values and deactivate fade panel
            fadingPanel.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
            fadeColor = fadingPanel.color;
            fadeout.SetActive(false);
            respawningText.SetActive(false);

            // Reset players transform and refill health
            transform.position = playerController.GetSpawnPosition();
            currentHealth = initialHealth;
            respawning = false;

            animationCanBePlayed = true;
        }
    }

    /**
     * Function which updates a flashing red frame in screen if player health is below
     * 20%. From 20% to 10%, the blinking will gradually increase speed between the blinking,
     * and a constant speed from 10% to 0% 
     **/
    private void UpdateFlashingFrame()
    {
        // Timer used to activate and deactivate warning panel for low health 
        if (timerActive)
            timer += Time.deltaTime;

        // When health goes from >20 to <=20
        if (currentHealth <= 20 && !timerActive)
        {
            lowHealthWarning.SetActive(!warningActive);
            timerActive = true;
            warningActive = !warningActive;
        }
        // If Health is below 20% and the timer has exceeded timeLimit variable

        else if (currentHealth <= 20f && timer >= timerLimit)
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

        else if (currentHealth >= 20f && timerActive)
        {
            // Disable warning and reset timer
            lowHealthWarning.SetActive(false);
            timerActive = false;
            timer = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float fallDamage = playerController.verticalVel;
        if (collision.gameObject.CompareTag("Terrain") && fallDamage <= -30.0f)
        {
            print("Fall damage");
            currentHealth += fallDamage / 1.5f;
        }
    }

    IEnumerator WaterDamage() {
        currentHealth -= 0.5f;
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator LavaDamage() {
        currentHealth -= 2.0f;
        yield return new WaitForSeconds(1.0f);
    }
}


