using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    /* Offset between player and camera */
    private Vector3 offset;

    /*Rotating camera by clicking and dragging mouse*/
    private bool mouseButtonHeldDown = false;
    public float turnSpeed = 4.0f;

    

    // Use this for initialization
    void Start()
    {
        offset = new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z + 7.0f);
    }


    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            mouseButtonHeldDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseButtonHeldDown = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mouseButtonHeldDown)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        }
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);

    }

}