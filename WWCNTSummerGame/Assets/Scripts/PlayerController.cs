using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    public bool grounded = true;
    public bool boosting = true;

    public Camera cam;
    public Interactable focus;
    public LayerMask movementMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

    }

    private void Update() {
        // Allows the player to jump
        if (Input.GetButtonDown("Jump") & grounded==true) {
            rb.AddForce(new Vector3(0,5,0), ForceMode.Impulse);
            grounded = false;
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
}

