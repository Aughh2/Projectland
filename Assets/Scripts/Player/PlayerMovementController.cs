using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float movementSpeed;
    public float speedMultiplier = 1f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetBool("isDead"))
        {
            return;
        }
        float speedX = Input.GetAxisRaw("Horizontal");
        float speedY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(speedX, speedY).normalized * movementSpeed * speedMultiplier;
        if (movement.magnitude > 0.1f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement;
    }
}
