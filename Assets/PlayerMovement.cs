using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;

    public float speed = 6f;
    public float jogSpeedMultiplier = 0.75f;
    public float sprintSpeedMultiplier = 1.5f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public Transform legSphere;
    public LayerMask ground;

    bool isGrounded;
    bool isSprinting;
    bool isJogging;
    float turnSmoothVelocity;
    Vector3 velocity;

    RewindPlayer rp;

    void Start()
    {
        rp = GetComponent<RewindPlayer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isGrounded = Physics.CheckSphere(legSphere.transform.position, 1f, ground);
        isSprinting = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && direction.magnitude >= 0.1f;
        isJogging = !isSprinting && direction.magnitude >= 0.1f;

        UpdateMovement(direction);

        if (!rp.isRewinding)
        {
            UpdateAnimatorParameters(direction.magnitude);
            HandleJumpInput();
        }

        ApplyGravity();
    }

    void UpdateMovement(Vector3 direction)
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float currentSpeed = isSprinting ? speed * sprintSpeedMultiplier : (isJogging ? speed * jogSpeedMultiplier : speed);

            controller.Move(moveDir * currentSpeed * Time.deltaTime);
        }
    }

    void UpdateAnimatorParameters(float moveMagnitude)
    {
        animator.SetBool("isSprinting", isSprinting);
        animator.SetBool("isJogging", isJogging);
        animator.SetBool("isJumping", !isGrounded);
    }

    void HandleJumpInput()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}