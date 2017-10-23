using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*The transform of the player*/
    public Transform playerTransform;

    /*Rotating camera by clicking and dragging mouse*/
    private bool mouseButtonIsHeldDown = false;

    //The endpoints of how far the camera can rotate vertically
    private const float Y_MIN = 0.0f;
    private const float Y_MAX = 50f;

    /*The endpoints of how far the camera can zoom in and out*/
    private const float SCROLL_MIN = 1.5f;
    private const float SCROLL_MAX = 50f;

    /*Variables used to control the behaviour of the camera*/
    private float distanceToCamera = 7.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float scrollAmount = 0.0f;
    private float turnSpeedX = 4.0f;
    private float turnSpeedY = 1.0f;
    private float scrollSpeed = 4.0f;


    private void Update()
    {
        //Register when left mousebutton is pushed down
        if (Input.GetMouseButtonDown(0))
        {
            mouseButtonIsHeldDown = true;
        }
        //Register when left mousebutton is released
        if (Input.GetMouseButtonUp(0))
        {
            mouseButtonIsHeldDown = false;
        }

        if (mouseButtonIsHeldDown)
        {
            //Get mouse coordinates
            currentX += Input.GetAxis("Mouse X") * turnSpeedX;
            currentY += Input.GetAxis("Mouse Y") * turnSpeedY;

            //Disable camera from rotating below Y-MIN and above Y-MAX
            currentY = Mathf.Clamp(currentY, Y_MIN, Y_MAX);
        }

        scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * -1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Set distance based on the player scrolling mouse wheel
        scrollAmount *= (distanceToCamera * 0.3f);
        distanceToCamera += scrollAmount;
        distanceToCamera = Mathf.Clamp(distanceToCamera, SCROLL_MIN, SCROLL_MAX);

        //Rotate camera and look at the player
        Vector3 direction = new Vector3(0, 0, -distanceToCamera);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = playerTransform.position + rotation * direction;
        transform.LookAt(playerTransform.position);
    }

}