using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class controlling all health related behaviour of the game character
public class PlayerHealth : MonoBehaviour {

    PlayerController playerController;

    private float initialHealth = 100.0f;
    private float healthDecrease = 2.0f;
    public float currentHealth;


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
}
