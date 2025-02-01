using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Playerinput Playerinput;
    public Playerinput.OnFootActions OnFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Playerinput = new Playerinput();
        OnFoot = Playerinput.OnFoot; // Correctly referencing the OnFoot property from Playerinput
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        OnFoot.Jump.performed += ctx => motor.jump();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        Vector2 moveInput = OnFoot.Movement.ReadValue<Vector2>();
        motor.ProcessMove(moveInput, debug: false); // Pass 'true' for debugging if needed
    }
    private void LateUpdate()
    {
        // Get the look input (mouse or right stick movement)
        Vector2 lookInput = OnFoot.Look.ReadValue<Vector2>();

        // Pass the input to the ProcessLook method
        look.ProcessLook(lookInput); // Now passing the correct Vector2 input
    }

    private void OnEnable()
    {
        OnFoot.Enable(); // Enable the OnFoot input actions
    }

    private void OnDisable()
    {
        OnFoot.Disable(); // Disable the OnFoot input actions
    }
}
