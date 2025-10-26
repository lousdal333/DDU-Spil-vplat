using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Movement input
        var keyboard = Keyboard.current;
        float move = 0f;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                move = -1f;
            else if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                move = 1f;
        }

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Ground check using a raycast from the collider bottom
        isGrounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);

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
    }
}
