using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PempekMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float verticalSpeed = 5f;
    public float rotationSpeed = 5f;
    public float smoothDamp = 0.3f;

    private Rigidbody rb;
    private float targetVerticalSpeed = 0f;
    private float currentVerticalSpeed = 0f;
    private float verticalVelocity = 0f;
    public Animator propellerAnimator;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    void FixedUpdate()
    {

        Vector3 movement = Vector3.zero;
        bool isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward * moveSpeed * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement -= transform.right * moveSpeed * Time.deltaTime;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right * moveSpeed * Time.deltaTime;
            isMoving = true;
        }

        // Input naik (Space) dan turun (E)
        if (Input.GetKey(KeyCode.Space))
        {
            targetVerticalSpeed = verticalSpeed;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            targetVerticalSpeed = -verticalSpeed;
            isMoving = true;
        }
        else
        {
            targetVerticalSpeed = 0f;
        }


        currentVerticalSpeed = Mathf.SmoothDamp(currentVerticalSpeed, targetVerticalSpeed, ref verticalVelocity, smoothDamp);
        Vector3 verticalMovement = transform.up * currentVerticalSpeed * Time.deltaTime;


        rb.MovePosition(rb.position + movement + verticalMovement);


        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        propellerAnimator.SetBool("IsMoving", isMoving);
    }
}
