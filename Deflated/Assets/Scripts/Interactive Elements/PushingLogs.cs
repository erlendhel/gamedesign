using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingLogs : MonoBehaviour {


    //public GameObject gameObject;
    private int numberOfLogs = 3;
    //private Rigidbody rb;
    private int counter = 0;

    private bool logs;

    

    private void Update()
    {
        if (counter >= 3)
        {
            print("Hello");
            
        }
    }

    private void OnCollisionExit(Collision log)
    {
        if (log.gameObject.CompareTag("Log"))
        {
            counter += 1;
        }

    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && counter >= 3)
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(0f, 1500f, 0f);
        }
    }


}
         






