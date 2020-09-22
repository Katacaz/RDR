using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        moveDirection = new Vector3(h, 0, v);


        animator.SetFloat("Speed", v);

        transform.Rotate(Vector3.up, h * turnSpeed * Time.deltaTime);

        if (v != 0)
        {
            float moveSpeedToUse = v > 0 ? moveSpeed : backwardMoveSpeed;
            controller.SimpleMove(transform.forward * moveSpeedToUse * v);
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
        knockBackCounter = knockBackTime;

        direction = new Vector3(1f, 1f, 1f);

        moveDirection = direction * knockBackForce;
    }
}
