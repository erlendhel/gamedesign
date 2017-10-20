using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    /* Offset between player and camera */
    private Vector3 offset;

    /*Draggin camera by clicking and dragging mouse*/
    private bool mouseButtonHeldDown = false;
    private Vector3 mousePosOnClick;
    private Vector3 lastMousePos;
    public float speed = 0.025f;

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - player.transform.position;
	}


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseButtonHeldDown = true;
            mousePosOnClick = Input.mousePosition;
            lastMousePos = mousePosOnClick;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseButtonHeldDown = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if (mouseButtonHeldDown)
        {
            Vector3 currentMousePos = Input.mousePosition;
            if (lastMousePos != currentMousePos)
            {
                Vector3 diff = lastMousePos - currentMousePos;
                transform.Rotate(diff.y * speed, diff.x * speed, 0, Space.World);
            }
            lastMousePos = currentMousePos;
        }
        
    }
}
