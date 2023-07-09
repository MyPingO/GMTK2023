using System.Collections;
using UnityEngine;
using UnscriptedLogic.MathUtils;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedIncreaseFactor;
    [SerializeField] private float jumpForce;
    [Range(0, 100)][SerializeField] private int jumpChance;
    [SerializeField] private bool dead = false;
    [SerializeField] bool bouncing = false;
    [SerializeField] private float bounceForce;
    [SerializeField] private float startingXPosition = 0f;
    [SerializeField] private float distanceTravelled = 0f;

    [Header("Components")]
    [SerializeField] private CapsuleCollider2D hitbox;
    [SerializeField] private Animator animator;

    [Header("Movement Detection")]
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float previousXPosition = 0f;
    [SerializeField] private float previousYPosition = 0f;

    public float movementChangeX;
    public float movementChangeY;

    LayerMask platformLayer;

    private Rigidbody2D rb;
    private int jumpTriggerLayer;

    [SerializeField] private LevelSettingsSO levelSettings;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        levelSettings = Resources.Load<LevelSettingsSO>("LevelDetails");
        levelSettings.Events.OnPlayerHit += Die;
        levelSettings.Events.OnGameLost += Disable;
        levelSettings.Events.OnGameStart += Enable;
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
        if (dead) return;

        distanceTravelled = transform.position.x - startingXPosition;

        // Increase speed over time
        speed = initialSpeed + (distanceTravelled * speedIncreaseFactor);

        Score.instance.distance = distanceTravelled;

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

    void FixedUpdate()
    {
        if (dead) return;
        movementChangeX = Mathf.Abs(transform.position.x - previousXPosition);
        movementChangeY = Mathf.Abs(transform.position.y - previousYPosition);
        // Check if the player has moved more than the threshold in X direction
        if (movementChangeX < threshold)
        {
            // Raycast to detect obstruction
            Vector2 capsuleSize = new Vector2(hitbox.size.x, hitbox.size.y);
            RaycastHit2D hit = Physics2D.CapsuleCast(transform.position, capsuleSize, hitbox.direction, 0f, Vector2.right, 0.5f, platformLayer);
            // Draw the raycast for debugging
            Debug.DrawRay(transform.position, Vector2.right * 0.5f, Color.red);

            // Player has not moved more than the threshold in Y direction
            if ((movementChangeY < threshold && hit.collider != null))
            {
                Jump();
            }
            else if (movementChangeY >= threshold)
            {
                //slight nudge to the right to prevent getting stuck on the platform
                rb.AddForce(Vector2.right * 0.1f, ForceMode2D.Impulse);
            }
        }
        previousXPosition = transform.position.x;
        previousYPosition = transform.position.y;
    }

    void Jump()
    {
        // Make the player jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Die(Transform player)
    {
        animator.SetBool("Dead", true);
        dead = true;
        speed = 0;
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        //wait for one second
        StartCoroutine(DelayBeforeUpwardForce());
    }

    IEnumerator DelayBeforeUpwardForce()
    {
        // Wait for one second
        yield return new WaitForSeconds(1);

        rb.simulated = true;
        // Apply upward force
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        //wait for another 3 seconds
        yield return new WaitForSeconds(3);

        // Reset the player state
        ResetPlayer();
    }

    void ResetPlayer()
    {
        dead = false;
        animator.SetBool("Dead", false);
        speed = initialSpeed;
        rb.velocity = Vector2.zero;
        hitbox.enabled = true;
    }

    bool IsJumpingOrFalling()
    {
        return rb.velocity.y > 0 || rb.velocity.y < 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player should jump
        if (collision.gameObject.layer == jumpTriggerLayer)
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

    void Disable()
    {
        enabled = false;
    }

    void Enable()
    {
        enabled = true;
    }
}