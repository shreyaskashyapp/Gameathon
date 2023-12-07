using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 7.0f;
    public float turnSpeed = 100.0f; // Speed of turning
    public float junctionMovementSpeed = 3.0f;
    private CharacterController myCharacterController;
    public Animator animator;
    public LayerMask junctionLayer;
    public LayerMask ground;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    public GameObject legSphere;
    private bool isGrounded;
    private bool isJunction;
    private RewindPlayer rp;

    // Start is called before the first frame update
    void Start()
    {
        myCharacterController = GetComponent<CharacterController>();
        rp = GetComponent<RewindPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isJunction = Physics.CheckSphere(transform.position, 2f, junctionLayer);


        isGrounded = Physics.CheckSphere(legSphere.transform.position, 1f, ground);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the player grounded
        }

        // Handle Jump Input
        HandleJumpInput();

        if (!isJunction)
        {
            // Standard movement
            Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
            myCharacterController.SimpleMove(moveDirection.normalized * speed);
        }
        else
        {
            // Junction movement
            if (Input.GetKeyDown(KeyCode.A))
                transform.Rotate(new Vector3(0f, -90f, 0f));
            else if (Input.GetKeyDown(KeyCode.D))
                transform.Rotate(new Vector3(0f, 90f, 0f));

            myCharacterController.SimpleMove(transform.forward * junctionMovementSpeed);
        }

        // Apply velocity for jump
        velocity.y += gravity * Time.deltaTime;
        myCharacterController.Move(velocity * Time.deltaTime);

        // Update the animator
        if (direction.magnitude > 0.1f && !rp.isRewinding && isGrounded)
        {
            animator.SetBool("isSprinting", true);
        }
        else if (!rp.isRewinding && isGrounded)
        {
            animator.SetBool("isSprinting", false);
        }
    }

    IEnumerator JumpCoroutine()
{
    float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

    velocity.y = jumpVelocity;
    yield return new WaitForSeconds(1.1f);

    // Move the player 1 unit ahead
    transform.Translate(Vector3.forward * 1f);

    animator.SetBool("isJumping", false);
    animator.Play("idle");
}
    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("isJumping",true);
            StartCoroutine(JumpCoroutine());
        }
    }
}