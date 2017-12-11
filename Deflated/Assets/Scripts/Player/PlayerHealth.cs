using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class controlling all health related behaviour of the game character
public class PlayerHealth : MonoBehaviour {

    PlayerController playerController;
    public static PlayerHealth playerHealth;

    private float initialHealth = 100.0f;
    private float healthDecrease = 2.0f;
    public float currentHealth;

    private float smallIncrease = 10.0f;
    private float mediumIncrease = 15.0f;
    private float bigIncrease = 20.0f;


    // Use this for initialization
    private void Awake() {
        if (playerHealth == null) {
            playerHealth = this;
        }
    }

    void Start () {
        playerController = GetComponent<PlayerController>();
        currentHealth = initialHealth;
	}
	
	// Update is called once per frame
	void Update () {
        currentHealth -= healthDecrease * Time.deltaTime;
        if (currentHealth < 0) {
            transform.position = playerController.GetSpawnPosition();
            currentHealth = 100.0f;
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

    private void OnCollisionEnter(Collision collision) {
        float fallDamage = playerController.verticalVel;
        if (collision.gameObject.CompareTag("Terrain") && fallDamage <= -20.0f) {
            print("Fall damage");
            currentHealth += fallDamage / 1.5f;
        }
    }
}
