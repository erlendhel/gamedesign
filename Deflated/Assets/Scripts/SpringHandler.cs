using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringHandler : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        print("Collision detected with trigger object " + other.name);
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(ScaleOverTime(1.0f));
        }
    }
    IEnumerator ScaleOverTime(float time) {
        Vector3 originalScale = gameObject.transform.localScale;
        Vector3 destinationScale = new Vector3(0.5f, 0.3f, 0.5f);

        float currentTime = 0.0f;

        do {
            gameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }
}