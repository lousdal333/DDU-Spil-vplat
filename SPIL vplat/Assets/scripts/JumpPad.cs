using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class JumpTile2D : MonoBehaviour
{
    [Tooltip("How strong the upward boost is.")]
    public float boostForce = 12f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null)
            {
                // Reset Y velocity for consistent height
                Vector2 v = rb.linearVelocity;
                v.y = 0f;
                rb.linearVelocity = v;

                // Apply upward impulse
                rb.AddForce(Vector2.up * boostForce, ForceMode2D.Impulse);
            }
        }
    }
}
