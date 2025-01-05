using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float chaseSpeed = 3f;
    public float attackCooldown = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public int minDamage = 3;
    public int maxDamage = 6;

    private Transform player;
    private Vector2 patrolTarget;
    private Animator animator;
    private Rigidbody2D rb;
    private float attackTimer = 0f;
    private bool isPatrolling = true;
    private bool isIdle = true;

    private Vector2 lastPosition;

    public LayerMask wall;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        SetRandomPatrolTarget();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (player == null || !enabled)
            return;

        attackTimer += Time.deltaTime;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange)
        {
            HuntPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolTarget) < 0.1f)
        {
            Idle();
            Invoke(nameof(SetRandomPatrolTarget), 2f);
        }
        else
        {
            MoveTowards(patrolTarget, walkSpeed);
        }
    }

    void ChasePlayer()
    {
        isIdle = false;
        MoveTowards(player.position, chaseSpeed);
    }

    void HuntPlayer()
    {
        isIdle = false;
        animator.SetBool("isMoving", false);

        if (attackTimer >= attackCooldown)
        {
            attackTimer = 0f;
            animator.SetTrigger("Attack");
        }
    }

    void Idle()
    {
        if (!isIdle)
        {
            isIdle = true;
            animator.SetBool("isMoving", false);
        }
    }

    public void PerformAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            int damage = Random.Range(minDamage, maxDamage + 1);
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    void SetRandomPatrolTarget()
    {
        patrolTarget = (Vector2)transform.position + Random.insideUnitCircle * 5f;
        isIdle = false;
        animator.SetBool("isMoving", true);
    }

    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, wall);

        if (hit.collider == null || hit.distance > speed * Time.fixedDeltaTime)
        {
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
        else
        {
            Debug.Log($"Obstacle detected: {hit.collider.name}");
            SetRandomPatrolTarget();
        }

        bool isMoving = Vector2.Distance(transform.position, lastPosition) > 0.01f;
        animator.SetBool("isMoving", isMoving);

        RotateTowards(target);
        lastPosition = transform.position;
    }


    void RotateTowards(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;

        if (direction.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            float currentAngle = rb.rotation;

            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * 10f);
            rb.MoveRotation(newAngle);
        }
    }
}
