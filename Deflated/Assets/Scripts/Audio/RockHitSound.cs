using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHitSound : MonoBehaviour {


    private void OnCollisionEnter(Collision other)
    {
        // If a movable rock is hit by the player with velocity higher than 1
        if (other.gameObject.CompareTag("Player") && other.rigidbody.velocity.magnitude >= 1f)
        {
            // Play a hitting sound
            AudioManager.instance.Play("RockHit");

            // If the velocity is over 4, also play the sound of a rock moving
            if (other.rigidbody.velocity.magnitude >= 4f)
                AudioManager.instance.Play("MovingRock");
        }
        
        
    }

}
