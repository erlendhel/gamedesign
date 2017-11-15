using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSound : MonoBehaviour {

    public Transform player;
    private Rigidbody playerRigidBody;
    private PlayerController playerController;

    private void Start()
    {
        playerRigidBody = player.gameObject.GetComponent<Rigidbody>();
        playerController = player.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(playerRigidBody.velocity.magnitude);
        if (other.gameObject.CompareTag("Player") && playerRigidBody.velocity.magnitude >= 3f && playerController.IsGrounded())
        {
            if(!AudioManager.instance.IsPlaying("GrassRustling"))
                AudioManager.instance.Play("GrassRustling");
        }

        if (playerRigidBody.velocity.magnitude <= 3f)
        {
            AudioManager.instance.Stop("GrassRustling");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.instance.Stop("GrassRustling");
    }
}
