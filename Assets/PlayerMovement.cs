using System.Collections;
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
    public GameObject legSphere;
    public LayerMask ground;
    public LayerMask junction;

    bool isGrounded;
    bool isSprinting;
    bool isJogging;
    public bool isJunction;
    float turnSmoothVelocity;
    bool isSliding;
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

        isGrounded = Physics.CheckSphere(legSphere.transform.position, 0.7f, ground);
        isSprinting = direction.magnitude >= 0.1f;
        isJunction = Physics.CheckSphere(transform.position, 2f, junction);

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.Play("slide");
            AnimatorStateInfo stateInfo21 = animator.GetCurrentAnimatorStateInfo(0);
            float animLength21 = stateInfo21.length;
            Invoke("slideReset", animLength21);
            isSliding = true;
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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            StartCoroutine(JumpCoroutine());
        }
    }

    IEnumerator JumpCoroutine()
    {
        animator.Play("jump");
        float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y = jumpVelocity;
        yield return new WaitForSeconds(2.2f);

        animator.Play("idle");

    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
    void slideReset(){
    animator.Play("Breathing Idle");
  }
}