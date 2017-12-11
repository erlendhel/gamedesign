using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBubbleHandler : MonoBehaviour {

    private SphereCollider collider;
    private MeshRenderer renderer;
    private float respawnTime = 30f;

	// Use this for initialization
	void Start () {
        collider = GetComponent<SphereCollider>();
        renderer = GetComponent<MeshRenderer>();
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // Disable mesh renderer and collider
            collider.enabled = false;
            renderer.enabled = false;
            StartCoroutine("PickupTimer");
        }
    }

    IEnumerator PickupTimer() {
        yield return new WaitForSeconds(respawnTime);
        // Respawn the airbubble after 30 seconds
        collider.enabled = true;
        renderer.enabled = true;
	}
}
