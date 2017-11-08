using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPickupSound : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Pickup");
    }
}
