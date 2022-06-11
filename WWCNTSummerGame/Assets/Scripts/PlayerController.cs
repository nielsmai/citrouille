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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Debug.Log(speed);

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
    }
    //This function prevents the player from jumping before touching the ground again
    private void OnCollisionEnter(Collision collision){
      grounded = true;
    }

}