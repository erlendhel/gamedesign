﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHandler : MonoBehaviour {

    public PlayerHealth playerHealth;
    public UpstreamHandler upstreamHandler;
    public ParticleSystem upstreamParticles;

    private void Start() {
        upstreamParticles.Stop();
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.inLava = true;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("LavaRock")) {
            print("Success");
            upstreamParticles.Play();
            upstreamHandler.yForce = 2750.0f;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.inLava = false;
        }
    }
}