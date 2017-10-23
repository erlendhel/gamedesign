using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    /* Offset between player and camera */
    private Vector3 offset;


    public Transform lookAt;
    public Transform cameraTransform;

    private const float Y_MIN = 0.0f;
    private const float Y_MAX = 50f;

    private Camera cam;

    private float distance = 7.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float turnSpeedX = 4.0f;
    private float turnSpeedY = 2.0f;


    /*Rotating camera by clicking and dragging mouse*/
    private bool mouseButtonHeldDown = false;

    

    // Use this for initialization
    void Start()
    {
        cameraTransform = transform;
        cam = Camera.main;
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

        if (mouseButtonHeldDown)
        {
            currentX += Input.GetAxis("Mouse X") * turnSpeedX;
            currentY += Input.GetAxis("Mouse Y") * turnSpeedY;

            currentY = Mathf.Clamp(currentY, Y_MIN, Y_MAX);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 dir = new Vector3(0, 0, -distance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        cameraTransform.position = lookAt.position + rotation * dir;
        cameraTransform.LookAt(lookAt.position);
        
    }

}