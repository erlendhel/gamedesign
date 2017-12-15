using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoButtonHandler : MonoBehaviour {

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("LavaRock")) {
            print("Rock off!");
        }
    }

}
