using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public int distanceRan;
    private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider2D;
    int jumpTriggerLayer;

    void Start()
    {
        // Get the Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        jumpTriggerLayer = LayerMask.NameToLayer("JumpTrigger");
        distanceRan = 0;
    }

    void Update()
    {
        // Make the player run
        rb.velocity = new Vector2(speed, rb.velocity.y);
        Score.instance.distance += speed * Time.deltaTime;
    }
    void Jump()
    {
        // Make the player jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player should jump
        if (collision.gameObject.layer == jumpTriggerLayer)
        {
            int jumpChance = Random.Range(0, 2);
            // 50/50 chance to jump
            if (jumpChance == 1)  {
                print("Jump!");
                Jump();
            }
            else {
                print("No jump!");
            }
        }
    }
}