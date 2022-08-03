using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script match Inputs from InputSystem with actions
// which are functions imported from each script

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    private PlayerThrowing playerThrowing;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        playerThrowing = GetComponent<PlayerThrowing>();

        // Button is pressed, Action is performed
        onFoot.Jump.performed += ctx => playerMotor.Jump();
        onFoot.Throw.performed += ctx => playerThrowing.Throw();

        //Set Cursor to not be visible & locked (Use Ctrl+P to exit Play Mode)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMotor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    void LateUpdate() {
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable() {
        onFoot.Enable();
    }

    private void OnDisable() {
        onFoot.Disable();
    }


}
