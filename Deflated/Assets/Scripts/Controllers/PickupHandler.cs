using UnityEngine;

/**
 *  Class which controls the behaviour of the pickups in the game. When a player collides/picks up
 *  a pickup, the pickup should disappear from the game world, and respawn after a set time. The 
 *  pickup is also added to the player's inventory, this is handled in the PlayerInventory.cs script.
 **/
public class PickupHandler : MonoBehaviour {
    
    // Check if a gameObject with tag "Player" collides with the pickup to which the script is assigned.
    // If there is a collision, 'hide' the pickup until respawn.
    // TODO: Create respawn functionality
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            gameObject.SetActive(false);
        }
    }
}
