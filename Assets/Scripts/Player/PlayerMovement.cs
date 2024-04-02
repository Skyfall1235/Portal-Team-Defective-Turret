using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Uses original Input System

    public CharacterController controller;
    [SerializeField] float speed;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField] float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    public bool isTeleporting;

    // Update is called once per frame
    void Update()
    {
        if (!isTeleporting) // Only update movement when not teleporting
        {
            if (IsGrounded(velocity))
            {
                velocity.y = -2f;
            }

            Move();
            Crouch(controller);

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded(velocity))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    private bool IsGrounded(Vector3 velocity)
    {

        if(Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) && velocity.y < 0)
        {
            return true;
        }
        return false;
    }

    private void Move()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / 2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    private void Crouch(CharacterController controller)
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            controller.height /= 2;
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            controller.height *= 2;
        }
    }
}
