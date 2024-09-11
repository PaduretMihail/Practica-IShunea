using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    bool sprinting = false;
    public float speed = 3f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    private float acceleration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            sprinting = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            sprinting = false;
        }

        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");

        isGrounded = controller.isGrounded;

        if(sprinting && speed < 9f){
            speed += acceleration * Time.deltaTime;
        }
        if(sprinting && speed > 9f){
            speed = 9f;
        }
        if(!sprinting && speed > 3f){
            speed -= acceleration * Time.deltaTime;
        }
        if(!sprinting && speed < 3f){
            speed = 3f;
        }
        if(!sprinting && !forwardPressed && !leftPressed && !rightPressed && !backPressed){
            speed = 3f;
        }

        if(Input.GetKey(KeyCode.Space)){
            if(isGrounded){
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }

    }
    public void ProcessMove(Vector2 input){
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

}
