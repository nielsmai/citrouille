using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public float jumpHeight = 3;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public bool grounded = true;
    public bool boosting = true;

    public Camera cam;
    public Interactable focus;
    public LayerMask movementMask;

    // Character controller
    CharacterController controller;
    private float ySpeed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded;
    private float gravity = -9.81f * 2f;

    // Keep track of the number of pumpkins picked up
    public static int numPumpkins = 0;

    // Boolean for inventory opening
    public bool inventoryOpen = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        // Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        // Vector3 velocity = new Vector3(movement.x * speed, ySpeed, movement.z * speed);
        // controller.Move(velocity * Time.deltaTime);


        // rb.AddForce(movement * speed);

    }

    private void Update() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0.0f, z).normalized;

        if (move.magnitude > 0.1f)
        {
            anim.SetTrigger("Moving");
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            controller.Move(move * speed * Time.deltaTime); 
        }       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetTrigger("Jumping");
        }
        // Our gravity value is doubled to make the player fall faster
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Check if inventory is open
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryOpen = !inventoryOpen;
        }
        // Prevent moving while inventory is open
        if (inventoryOpen)
        {
            movementX = 0;
            movementY = 0;
        }
      // Allows the player to go fast
        if (Input.GetButton("Speed")) {
            speed = 20;
            boosting = true;
        }
        else {
            speed = 10;
            boosting = false;
        }
        //Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) {
            //Case out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Check if the ray hits anything
            if (Physics.Raycast(ray, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    SetFocus(interactable);
                    // Debug.Log("Hit Interact " + hit.collider.name + " " + hit.point);

                }
            }
            
    }
}
    //This function prevents the player from jumping before touching the ground again
    private void OnCollisionEnter(Collision collision){
      grounded = true;
    }

    private void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null) {
                focus.OnDefocused();
            }
            focus = newFocus;
            newFocus.OnFocused(transform);
        }
    }
    private void RemoveFocus() {
        if (focus != null) {
            focus.OnDefocused();
        }
        focus = null;
    }

    // Functions to save and load player data
    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null) {
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
    }
    public static bool pickUpPumpkin() {
        numPumpkins++;
        if (numPumpkins >= 10) {
            // Get position of fox
            // Vector3 foxPos = GameObject.Find("Fox").transform.position;
            // Hide fox
            GameObject.Find("Fox").SetActive(false);
            // // Show FoxEnd gameobject
            // GameObject.Find("FoxEnd").SetActive(true);
            // // Set fox end position to fox position
            // GameObject.Find("FoxEnd").transform.position = foxPos;
            return true;
        }
        return true;
    }
    // Function to access the number of pumpkins
    public static int getNumPumpkins() {
        return numPumpkins;
    }
}


