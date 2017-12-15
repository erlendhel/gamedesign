using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 *  Class controlling the movement of a player in the game.  
 **/
public class PlayerController : MonoBehaviour {

    public bool isGrounded = true;
    public bool inWater = false;
    public bool canJump = true;
    public Rigidbody rb;
    public Animator teleAnim;
    public Vector3 currentPos;

    public float speed = 15f;
    public float jumpSpeed = 500.0f;
    public float vel;
    public float verticalVel;

    public bool swinging = false;
    int swingForce = 25;

    // Default values of game character
    public float initMass;
    public Vector3 initScale;

    public Vector3 initGrav = new Vector3(0, -9.81f, 0);

    public Vector3 spawnPosition;

    private bool rollingSoundStarted = false;

    public bool hasKey = false;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        CurrencyManager.currencyManager.Load();
        teleAnim = GetComponent<Animator>();
        teleAnim.enabled = false;
        initScale = transform.localScale;
        initMass = rb.mass;
        spawnPosition = rb.position;
        Physics.gravity = initGrav;
        print(Application.persistentDataPath);
    }

    private void Update() {

        vel = rb.velocity.magnitude;
        verticalVel = rb.velocity.y;
        currentPos = rb.position;
        if (Input.GetKeyDown("space") && isGrounded && canJump) {
            Jump();
        }
        if (Input.GetKeyDown("space") && swinging) {
            Destroy(GetComponent<HingeJoint>());
        }
    }

    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Make player follow the direction of the camera
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0.0f;

        movement = Vector3.Normalize(movement);

        if (inWater == false) {
            rb.AddForce(movement * speed);
        } else {
            rb.AddForce(movement * (speed / 10));
        }


        // Set sound for the ball rolling
        if (rb.velocity.magnitude >= 3f && !rollingSoundStarted && isGrounded)
        {
            //Used on first time entering this if-condition, because the sound should only be started once
            AudioManager.instance.Play("Rolling");
            rollingSoundStarted = true;
        }
        else if (AudioManager.instance.IsPlaying("Rolling") && (rb.velocity.magnitude < 3f || !isGrounded))
        {
            AudioManager.instance.Pause("Rolling");
        }
        else if (!AudioManager.instance.IsPlaying("Rolling") && rb.velocity.magnitude >= 3f && isGrounded)
        {
            AudioManager.instance.UnPause("Rolling");
        }
        


    }

    /*
     *  Function which adds force upwards, thus producing a jump.
     *  The variable jumpSpeed can be altered to produce effects
     *  of buffs, pickups etc.  
     */
    private void Jump() {
        AudioManager.instance.Play("Jump");
        rb.AddForce(Vector3.up * jumpSpeed);
    }

    /*
     *  Functions 'OnCollisionEnter' and 'OnCollisionExit' detects
     *  if the game character is on or off the ground. Used to prevent
     *  weird in-air behaviour such as double jumps.
     */
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Terrain")) {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Rope")) {
            swinging = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // To prevent bugs where the player sometimes are not able
        // to jump even though it has collided with a terrian tagged object
        if (collision.gameObject.CompareTag("Terrain"))
        {
            if(!isGrounded)
                isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Water")) {
            inWater = true;
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("River")) {
            inWater = true;
            isGrounded = false;
        }
    }

    // Function to detect if the game character is off the ground
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Terrain")) {
            isGrounded = false;
        } else if (collision.gameObject.CompareTag("Water")) {
            inWater = false;
        } else if (collision.gameObject.tag == "Rope") {
            swinging = true;

            // Add a hingejoint to our player
            HingeJoint hinge = gameObject.AddComponent<HingeJoint>() as HingeJoint;
            // Telling the hingejoint what to connet to
            hinge.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            hinge.connectedAnchor = new Vector3(0f, -0.9f,0f);

            
        }
    }

    public Vector3 GetSpawnPosition() {
        return spawnPosition;
    }

    public bool IsGrounded() {
        return isGrounded;
    }
    public void SetRespawnPosition(Vector3 position) {
        spawnPosition = position;
    }
}