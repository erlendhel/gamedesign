using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class setting the respawn point of the player when entering a zone
 * The zones found in the game are the different islands
 * **/
public class RespawnHandler : MonoBehaviour {

    public GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        playerController = player.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerController.SetRespawnPosition(gameObject.transform.position);
    }

}
