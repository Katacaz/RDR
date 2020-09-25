using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    CharacterController controller;
    private Animator animator;

    public float moveSpeed = 5.0f;
    public float backwardMoveSpeed = 3.0f;
    public float turnSpeed = 5.0f;
    Vector3 moveDirection;

    [Header("Knockback Related")]
    public bool isBeingKnockedBack;
    public float knockBackForce = 10f;
    public float knockBackTime;
    private float knockBackCounter;

    [Header("Camera Related")]
    public GameObject followTransform;
    public float cameraRotatePower = 5f;
    public bool usingOldInputSystem;
    float h = 0;
    float v = 0;
    float lookH;
    float lookV;

    [Header("Boost Related")]
    public float boostAmount = 10f;
    public bool canBoost;
    public float boostTimer;
    float boostCounter;

    public bool isGrounded;

    float vSpeed;
    public float gravity = 9.8f;
    public float jumpSpeed = 8;
    bool isJumping;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (knockBackCounter <= 0)
        {
            isBeingKnockedBack = false;
            isGrounded = controller.isGrounded;
            if (isGrounded)
            {
                vSpeed = 0f;
                if (isJumping)
                {
                    vSpeed = jumpSpeed;
                    isJumping = false;
                }
            }
            else
            {
                vSpeed -= gravity * Time.deltaTime;
            }
            vSpeed -= gravity * Time.deltaTime;
            moveDirection.y = vSpeed;
            if (usingOldInputSystem)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");


            }
            //moveDirection = new Vector3(h, moveDirection.y, v);
            moveDirection = transform.forward * v + transform.up * moveDirection.y + transform.right * h;
        } else
        {
            knockBackCounter -= Time.deltaTime;
            isBeingKnockedBack = true;
        }
        animator.SetFloat("Speed", v);

        //transform.Rotate(Vector3.up, h * turnSpeed * Time.deltaTime);
        float moveSpeedToUse = moveSpeed;
        if (v != 0)
        {
            moveSpeedToUse = v > 0 ? moveSpeed : backwardMoveSpeed;
            //controller.SimpleMove(transform.forward * moveSpeedToUse * v);
        }
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        followTransform.transform.rotation *= Quaternion.AngleAxis(lookH * cameraRotatePower, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(lookV * cameraRotatePower, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;
        var angle = followTransform.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        } else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTransform.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0f, followTransform.transform.rotation.eulerAngles.y, 0f);
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        if (boostCounter <= 0)
        {
            canBoost = true;

        } else
        {
            canBoost = false;
            boostCounter -= Time.deltaTime;
        }

        /*controller.SimpleMove(moveDirection * Time.deltaTime * moveSpeed);

        animator.SetFloat("Speed", moveDirection.magnitude);

        if (moveDirection.magnitude > 0)
        {
            Quaternion newDirection = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
        }

        if (knockBackCounter <= 0)
        {
            isBeingKnockedBack = false;
        } else
        {
            isBeingKnockedBack = true;
            knockBackCounter -= Time.deltaTime;
        }*/
    }

    public void KnockBack(Vector3 direction)
    {
        if (!isBeingKnockedBack)
        {
            knockBackCounter = knockBackTime;

            moveDirection = direction * knockBackForce;
            //Debug.Log("Player Knocked Back");
        }
    }

    public void OnMovement(InputValue value)
    {
        Vector2 inputMovement = value.Get<Vector2>();
        h = inputMovement.x;
        v = inputMovement.y;
    }
    public void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        lookH = input.x;
        lookV = input.y;
    }

    public void OnBoost()
    {
        if (canBoost)
        {
            boostCounter = boostTimer;
            
        }
    }

    public void OnJump()
    {
        if (controller.isGrounded)
        {
            isJumping = true;
        }
    }

    public void StopKnockBack()
    {
        knockBackCounter = 0;
    }
}
