using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Collision Checks")]
    public float groundCheckDistance = 0.05f;
    public float wallCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isGrounded;
    private bool isTouchingWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        float move = 0f;

        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                move = -1f;
            else if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                move = 1f;
        }

        // --- Ground Check (small box slightly below player) ---
        Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, col.bounds.size.y * 0.05f);
        Vector2 boxCenter = (Vector2)col.bounds.center + Vector2.down * (col.bounds.extents.y + groundCheckDistance * 0.5f);
        isGrounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);

        // --- Wall Check (small vertical box to sides) ---
        Vector2 dir = new Vector2(move, 0f);
        if (move != 0)
        {
            Vector2 wallBoxSize = new Vector2(col.bounds.size.x * 0.05f, col.bounds.size.y * 0.9f);
            Vector2 wallBoxCenter = (Vector2)col.bounds.center + dir * (col.bounds.extents.x + wallCheckDistance * 0.5f);
            isTouchingWall = Physics2D.OverlapBox(wallBoxCenter, wallBoxSize, 0f, groundLayer);
        }
        else
        {
            isTouchingWall = false;
        }

        // --- Move ---
        if (!isTouchingWall)
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        // --- Jump ---
        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (col == null) return;

        Gizmos.color = Color.yellow;
        Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, col.bounds.size.y * 0.05f);
        Vector2 boxCenter = (Vector2)col.bounds.center + Vector2.down * (col.bounds.extents.y + groundCheckDistance * 0.5f);
        Gizmos.DrawWireCube(boxCenter, boxSize);

        Gizmos.color = Color.red;
        Vector2 rightCenter = (Vector2)col.bounds.center + Vector2.right * (col.bounds.extents.x + wallCheckDistance * 0.5f);
        Vector2 leftCenter = (Vector2)col.bounds.center + Vector2.left * (col.bounds.extents.x + wallCheckDistance * 0.5f);
        Vector2 wallBoxSize = new Vector2(col.bounds.size.x * 0.05f, col.bounds.size.y * 0.9f);
        Gizmos.DrawWireCube(rightCenter, wallBoxSize);
        Gizmos.DrawWireCube(leftCenter, wallBoxSize);
    }
}
