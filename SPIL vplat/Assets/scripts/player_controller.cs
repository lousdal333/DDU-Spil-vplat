using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f;
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

        // Check if grounded
        isGrounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);

        // Check for wall *only* on sides, not floor
        Vector2 dir = new Vector2(move, 0f);
        if (move != 0)
        {
            RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, dir, wallCheckDistance, groundLayer);
            isTouchingWall = hit && Mathf.Abs(hit.normal.y) < 0.1f; // ensures it's a side wall, not floor/ceiling
        }
        else
        {
            isTouchingWall = false;
        }

        // Move
        if (!isTouchingWall)
            rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        // Jump
        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (col == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(col.bounds.center + Vector3.down * groundCheckDistance, col.bounds.size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col.bounds.center + Vector3.right * wallCheckDistance, col.bounds.size);
        Gizmos.DrawWireCube(col.bounds.center + Vector3.left * wallCheckDistance, col.bounds.size);
    }
}

