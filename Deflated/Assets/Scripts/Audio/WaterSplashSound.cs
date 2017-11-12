using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplashSound : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.Play("WaterSplash");
        }
    }
}
