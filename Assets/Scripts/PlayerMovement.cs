using UnityEngine;
using UnscriptedLogic.MathUtils;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [Range(0, 100)] [SerializeField] private int jumpChance = 50;

    [Header("Components")]
    [SerializeField] private BoxCollider2D boxCollider2D;

    private Rigidbody2D rb;
    private int jumpTriggerLayer;

    void Start()
    {
        // Get the Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        jumpTriggerLayer = LayerMask.NameToLayer("JumpTrigger");
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
            int rolledJumpChance = Random.Range(0, 100); ;
            if (rolledJumpChance <= jumpChance)  {
                Debug.Log("Jump!");
                Jump();
            }
            else {
                Debug.Log("No jump!");
            }
        }
    }
}