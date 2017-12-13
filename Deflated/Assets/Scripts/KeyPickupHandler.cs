﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupHandler : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}