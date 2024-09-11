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
        isGrounded = controller.isGrounded;
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            sprinting = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            sprinting = false;
        }
        if(sprinting && speed < 9f){
            speed += acceleration * Time.deltaTime;
        }
        if(sprinting && speed > 9f){
            speed = 9f;
        }
        if(!sprinting && speed > 3){
            speed -= acceleration * Time.deltaTime;
        }
        if(!sprinting && speed < 3){
            speed = 3f;
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

    public void Jump(){
        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Sprint(){
        sprinting = !sprinting;
        if(sprinting){
            speed = 40f;
        }else speed = 5f;
    }
}
