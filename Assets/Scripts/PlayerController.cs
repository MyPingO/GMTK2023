using UnityEngine;
using UnscriptedLogic.MathUtils;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedIncreaseFactor;
    [SerializeField] private float jumpForce;
    [Range(0, 100)][SerializeField] private int jumpChance;

    [SerializeField] bool bouncing = false;
    [SerializeField] private float bounceForce;
    [SerializeField] private float startingXPosition = 0f;
    [SerializeField] private float distanceTravelled = 0f;

    [Header("Components")]
    [SerializeField] private CapsuleCollider2D hitbox;
    [SerializeField] private Animator animator;

    LayerMask platformLayer;

    private Rigidbody2D rb;
    private int jumpTriggerLayer;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    void Start()
    {
        // Get the Rigidbody2D
        hitbox = GetComponent<CapsuleCollider2D>();
        jumpTriggerLayer = LayerMask.NameToLayer("JumpTrigger");
        startingXPosition = transform.position.x;
        platformLayer = LayerMask.GetMask("Platform");
        initialSpeed = speed;
    }

    void Update()
    {

        distanceTravelled = transform.position.x - startingXPosition;

        // Increase speed over time
        speed = initialSpeed + (distanceTravelled * speedIncreaseFactor);

        Score.instance.distance = distanceTravelled;

        // Raycast to detect obstruction
        Vector2 capsuleSize = new Vector2(hitbox.size.x, hitbox.size.y);
        RaycastHit2D hit = Physics2D.CapsuleCast(transform.position, capsuleSize, hitbox.direction, 0f, Vector2.right, 0.5f, platformLayer);
        bool notMoving = Mathf.Approximately(rb.velocity.x, 0);

        // Draw the raycast for debugging
        Debug.DrawRay(transform.position, Vector2.right * 0.5f, Color.red);

        // If obstruction is detected and player is not moving horizontally, trigger a jump
        if (!IsJumpingOrFalling() && notMoving)
        {
            Jump();
        }

        // Make the player run
        if (!bouncing) rb.velocity = new Vector2(speed, rb.velocity.y);

        // Update the Animator
        if (rb.velocity.y > 0)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Falling", false);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
            animator.SetBool("Jumping", false);
        }
        else
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
    }

    void Jump()
    {
        // Make the player jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Die() 
    {
        animator.SetBool("Dead", true);
    }

    bool IsJumpingOrFalling()
    {
        return rb.velocity.y > 0 || rb.velocity.y < 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player should jump
        if (collision.gameObject.layer == jumpTriggerLayer && !bouncing)
        {
            int rolledJumpChance = Random.Range(0, 100); ;
            if (rolledJumpChance <= jumpChance)
            {
                Debug.Log("Jump!");
                Jump();
            }
            else
            {
                Debug.Log("No jump!");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player should jump
        if (collision.gameObject.layer == LayerMask.NameToLayer("BouncyPlatform"))
        {
            bouncing = true;

            // Get the normal of the surface where the collision happened
            Vector2 normal = collision.GetContact(0).normal;
            if (Mathf.Abs(normal.y) <= Mathf.Abs(normal.x))
            {
                if (normal.x > 0)
                {
                    // The normal is mainly pointing to the right
                    rb.velocity = new Vector2(.1f, transform.position.y);
                }
                else if (normal.x < 0)
                {
                    // The normal is mainly pointing to the left
                    rb.velocity = new Vector2(-.1f, transform.position.y);
                }
            }
            // Apply force in the direction of the normal
            rb.AddForce((normal * bounceForce), ForceMode2D.Impulse);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            bouncing = false;
        }
    }
}