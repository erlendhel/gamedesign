using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupSound : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.Play("KeyPickup");
    }
}
