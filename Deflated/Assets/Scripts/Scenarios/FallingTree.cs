using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTree : MonoBehaviour {

    //The rigidbody to the top part of the tree
    private Rigidbody treeRigidBody;


    /* NOTE: this is a tempory solution to the top trunk not having
    a collider when rigidbody.detectCollisions is set to false
    
    Collider used for top trunk when rigidbody is disabled*/
    public Transform trunkCollider;

	void Start ()
    {
        // Get the rigidbody of the top part of the trunk and turn off gravity and collision 
        treeRigidBody = GetComponentInChildren<Rigidbody>();
        treeRigidBody.detectCollisions = false;
        treeRigidBody.useGravity = false;
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Get the physics of the ball/player
            Rigidbody player = other.gameObject.GetComponent<Rigidbody>();
            float playerVelocity = Vector3.Magnitude(player.velocity);
            float kineticEnergy =  (player.mass * Mathf.Pow(playerVelocity,2)) / 2;

            Debug.Log("Kinetic energy on collision with tree: " + kineticEnergy);

            // The the energy on crash is over 10
            if(kineticEnergy > 10f)
            {
                //Turn of trunk collider for when tree was still standing
                //NOTE: temporary solution
                trunkCollider.gameObject.SetActive(false);

                //Enable collision and gravity for top part of trunk
                treeRigidBody.detectCollisions = true;
                treeRigidBody.useGravity = true;
            }
        }
    }
}
