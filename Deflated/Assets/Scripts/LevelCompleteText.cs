using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteText : MonoBehaviour {


    public GameObject levelCompleteText;

    private void OnTriggerEnter(Collider other)
    {
        levelCompleteText.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        levelCompleteText.SetActive(false);
    }
}
