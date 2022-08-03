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


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => playerMotor.Jump();

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
