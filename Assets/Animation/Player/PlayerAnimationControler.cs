using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerAnimationControler : MonoBehaviour
{
    Animator animator;
    private CharacterController controller;
    private bool isGrounded;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    private bool isJumping = false;
    private bool isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");
        

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;
        if(isGrounded){
            if(forwardPressed && velocityZ < currentMaxVelocity){
                velocityZ += Time.deltaTime * acceleration;
            }
            if(!forwardPressed && velocityZ > 0.0f){
                velocityZ -= Time.deltaTime * deceleration;
            }
            if(leftPressed && velocityX > -currentMaxVelocity){
                velocityX -= Time.deltaTime * acceleration;
            }
            if(!leftPressed && velocityX < 0.0f){
                velocityX += Time.deltaTime * deceleration;
            }
            if(rightPressed && velocityX < currentMaxVelocity){
                velocityX += Time.deltaTime * acceleration;
            }
            if(!rightPressed && velocityX > 0.0f){
                velocityX -= Time.deltaTime * deceleration;
            }
            if(backPressed && velocityZ > -currentMaxVelocity){
                velocityZ -= Time.deltaTime * acceleration;
            }
            if(!backPressed && velocityZ < 0.0f){
                velocityZ += Time.deltaTime * deceleration;
            }
            if(!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)){
                velocityX = 0.0f;
            }
            if(!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f)){
                velocityZ = 0.0f;
            }

            if(forwardPressed && runPressed && velocityZ > currentMaxVelocity){
                velocityZ = currentMaxVelocity;
            }
            else if(forwardPressed && velocityZ > currentMaxVelocity){
                velocityZ -= Time.deltaTime * deceleration;
                if(velocityZ > currentMaxVelocity && velocityZ <(currentMaxVelocity +0.05)){
                    velocityZ = currentMaxVelocity;
                }
            }
            else if(forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f)){
                velocityZ = currentMaxVelocity;
            }
            
            if(jumpPressed && !runPressed){
                isJumping = true;
                isRunning = false;
            }
            if(jumpPressed && runPressed){
                isJumping = true;
                isRunning = true;
            }
            if(!jumpPressed){
                isJumping = false;
                isRunning = false;
            }
            
        }

            animator.SetFloat("VelocityZ", velocityZ);
            animator.SetFloat("VelocityX", velocityX);
            animator.SetBool("isJumping", isJumping);
            animator.SetBool("isRunning", isRunning);

    }

}
