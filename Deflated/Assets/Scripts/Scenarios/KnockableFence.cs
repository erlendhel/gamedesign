using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableFence : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get physic data from player on collision
            Rigidbody player = collision.gameObject.GetComponent<Rigidbody>();
            float playerVelocity = Vector3.Magnitude(player.velocity);
            float kineticEnergy = ((player.mass * Mathf.Pow(playerVelocity,2)) / 2);

            Debug.Log("Kinetic energy on collison with fence: " + kineticEnergy);
            
            // If the player hits the fence with a kinetic force of more than 40J
            if (kineticEnergy > 10f)
            {
                // Turn of all constraints, so the fence will fall down
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
