using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ImputManager : MonoBehaviour
{
    private PlayerInputs playerInput;
    private PlayerInputs.WalkActions walk;

    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInputs();
        walk = playerInput.Walk;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        //walk.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(walk.Movement.ReadValue<Vector2>());
    }
    void LateUpdate(){
        look.ProcessLook(walk.Look.ReadValue<Vector2>());
    }
    private void OnEnable(){
        walk.Enable();
    }
    private void OnDisable(){
        walk.Disable();
    }

}
