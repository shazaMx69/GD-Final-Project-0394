using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
    }
    // receives the inputs for our InputManager.cs and apply them to our character.
    public void ProcessMove(Vector2 input, bool debug = false)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded&&playerVelocity.y<0)
            playerVelocity.y =-2f;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Log if debugging is enabled
        if (debug)
        {
            Debug.Log("Player Velocity Y: " + playerVelocity.y);
        }
    }
    public void jump()
    {
        if(isGrounded)
        {
            playerVelocity.y=Mathf.Sqrt(jumpHeight *-3.0f*gravity);
        }


    }

}

