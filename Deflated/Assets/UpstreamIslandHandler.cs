using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpstreamIslandHandler : MonoBehaviour {

    private Animator anim;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine("MoveIsland");
        }
    }

    IEnumerator MoveIsland() {
        yield return new WaitForSeconds(5);
        anim = GetComponent<Animator>();
        anim.Play("UpstreamIslandPlay");
    }

}
