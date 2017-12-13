using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class which controls the behaviour of the pickups in the game. When a player collides/picks up
 *  a pickup, the pickup should disappear from the game world, and respawn after a set time. The 
 *  pickup is also added to the player's inventory, this is handled in the PlayerInventory.cs script.
 **/
public class PickupHandler : MonoBehaviour {
  
    private MeshRenderer[] renderers;
    private Collider collider;
    private float respawnTime = 30f;

    private void Start(){
        collider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<MeshRenderer>();
    }
    
    // Check if a gameObject with tag "Player" collides with the pickup to which the script is assigned.
    // If there is a collision, 'hide' the pickup until respawn.

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // Disable pickup and all meshrenderes in pickup 
            collider.enabled = false;
            foreach (MeshRenderer render in renderers) {
                render.enabled = false;
            }

            // Start timer to enable meshrenderes and colliders after certain time limit
            StartCoroutine("PickupTimer");
        }
    }

    IEnumerator PickupTimer() {
        yield return new WaitForSeconds(respawnTime);
        // Enable collider and meshrenderers after 30 seconds
        collider.enabled = true;
        foreach (MeshRenderer render in renderers) {
            render.enabled = true;
        }
    }
}
