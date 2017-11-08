using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePickupSound : MonoBehaviour {

    private void OnTriggerEnter(Collider collider)
    {
        AudioManager.instance.Play("BubblePickup");
    }
}
