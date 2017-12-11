using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour {

    private MeshRenderer[] renderers;
    private Collider collider;
    private float respawnTime = 30f;

    private void Start(){
        collider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        print(renderers.Length);
    }

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
