using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockoverBridgeHandler : MonoBehaviour {

    public Animator anim;
    public GameObject player;
    public PlayerController playerController;
    private bool isActive = false;
    public float playerVelocity;
    public float collisionForce;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
        anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        playerVelocity = playerController.vel;
	}

    private void OnCollisionEnter(Collision player) {
        collisionForce = (playerController.rb.mass * (Mathf.Pow(playerVelocity, 2))) / 2;
        if (player.gameObject.CompareTag("Player") && !isActive && collisionForce >= 2000.0f) {
            anim.enabled = true;
        }
    }
}
