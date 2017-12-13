using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockoverBridgeHandler : MonoBehaviour {

    public Animator anim;
    public GameObject player;
    public PlayerController playerController;
    private bool isActive = false;
    public float collisionForce;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
        anim.enabled = false;
	}
	
    private void OnCollisionEnter(Collision player) {
        collisionForce = (playerController.rb.mass * (Mathf.Pow(playerController.vel, 2))) / 2;
        if (player.gameObject.CompareTag("Player") && !isActive && collisionForce >= 3500.0f) {
            anim.enabled = true;
        }
    }
}
