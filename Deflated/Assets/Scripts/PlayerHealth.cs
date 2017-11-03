using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class controlling all health related behaviour of the game character
public class PlayerHealth : MonoBehaviour {

    PlayerController playerController;

    private float initialHealth = 100.0f;
    private float healthDecrease = 2.0f;
    public float currentHealth;

    private float smallIncrease = 10.0f;
    private float mediumIncrease = 15.0f;
    private float bigIncrease = 20.0f;


	// Use this for initialization
	void Start () {
        playerController = GetComponent<PlayerController>();
        currentHealth = initialHealth;
	}
	
	// Update is called once per frame
	void Update () {
        currentHealth -= healthDecrease * Time.deltaTime;
        if (currentHealth < 0) {
            transform.position = playerController.getSpawnPosition();
            currentHealth = 100.0f;
        }
	}

    private void OnTriggerEnter(Collider bubble) {
        if (bubble.gameObject.CompareTag("SmallBubble")) {
            currentHealth += smallIncrease;
        } else if (bubble.gameObject.CompareTag("MediumBubble")) {
            currentHealth += mediumIncrease;
        } else if (bubble.gameObject.CompareTag("LargeBubble")) {
            currentHealth += bigIncrease;
        }
    }
}
