using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    //private Rigidbody rB;

    public float jumpForce = 5.0f;

    private CharacterController controller;

    Vector3 moveDirection;

    public float fallScale = 1.0f;

    public bool jumpCharging;
    public float jumpChargeAmount;
    public float jumpChargeScale = 1f;
    public float jumpChargeMin;
    public float jumpChargeMax;
    float flattenScale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //rB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (!jumpCharging)
        //{
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        //} else
        //{
        //    moveDirection = Vector3.zero;
        //}
        float yStore = moveDirection.y;
        if (controller.isGrounded)
        {
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
        } else
        {
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
        }
        moveDirection.y = yStore;
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButton("Jump"))
            {
                if (!jumpCharging)
                {
                    jumpChargeAmount = jumpChargeMin;
                }
                jumpCharging = true;

            }
            if (!Input.GetButton("Jump"))
            {
                jumpCharging = false;
            }

            if (!jumpCharging)
            {
                moveDirection.y = jumpChargeAmount;
                jumpChargeAmount = 0f;
                transform.localScale = new Vector3(1f, 1f, 1f);
            } else
            {
                if (jumpChargeAmount < jumpChargeMax)
                {
                    jumpChargeAmount += jumpChargeScale * Time.deltaTime;
                    if (transform.localScale.y > 0.3f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - (flattenScale * Time.deltaTime), transform.localScale.z);
                    }
                }
            }
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * fallScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);


    }
}
