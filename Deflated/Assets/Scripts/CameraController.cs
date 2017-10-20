using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    /* Offset between player and camera */
    private Vector3 offset;

    /*Draggin camera by clicking and dragging mouse*/
    private bool mouseButtonHeldDown = false;
    private Vector3 mousePosOnClick;
    private Vector3 lastMousePos;
    public float turnSpeed = 4.0f;

    public float timer = 0;

    // Use this for initialization
    void Start()
    {
        //offset = transform.position - player.transform.position;
        offset = new Vector3(player.transform.position.x, player.transform.position.y + 6.0f, player.transform.position.z + 7.0f);
    }


    private void Update()
    {
        timer += Time.deltaTime * 3.5f;
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
        if (mouseButtonHeldDown)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        }
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);
        /*if (mouseButtonHeldDown)
        {
            Rotate();
        }*/

    }

    void Rotate()
    {
        Vector3 currentMousePos = Input.mousePosition;
        if (lastMousePos != currentMousePos)
        {
            Vector3 diff = lastMousePos - currentMousePos;
            float x = -Mathf.Cos(timer) * 10f;
            float z = Mathf.Sin(timer) * 10f;
            Vector3 pos = new Vector3(x, 5f, z);
            transform.position = pos + player.transform.position;
            transform.LookAt(player.transform);
            //transform.Rotate(diff.y * turnSpeed, -diff.x * turnSpeed, 0, Space.World);
        }
        lastMousePos = currentMousePos;

    }
}