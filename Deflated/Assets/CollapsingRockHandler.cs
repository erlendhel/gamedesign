using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingRockHandler : MonoBehaviour {

    Rigidbody[] stones;
    public TopRockHandler topRock;

	// Use this for initialization
	private void Start () {
        stones = GetComponentsInChildren<Rigidbody>();
        topRock = stones[7].GetComponent<TopRockHandler>();
	}

    private void Update() {
        if (topRock.isActivated) {
            StartCoroutine("Collapse");
        }
    }

    IEnumerator Collapse() {
        yield return new WaitForSeconds(6);
        stones[7].isKinematic = false;
        yield return new WaitForSeconds(1.3f);
        stones[6].isKinematic = false;
        yield return new WaitForSeconds(1.3f);
        stones[5].isKinematic = false;
        yield return new WaitForSeconds(1.3f);
        stones[4].isKinematic = false;
        yield return new WaitForSeconds(1.3f);
        stones[3].isKinematic = false;
        yield return new WaitForSeconds(1.3f);
        stones[2].isKinematic = false;

    }
}
