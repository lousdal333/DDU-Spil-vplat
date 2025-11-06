using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class TilemapVelocityReversal : MonoBehaviour
{
    [SerializeField] private float multiplier = 2f; // 2x speed in opposite direction

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb == null) return;

        // We’ll try to use the relativeVelocity Unity provides
        Vector2 incomingVelocity = -collision.relativeVelocity;
        // relativeVelocity is the velocity of the collider *before* the collision response

        // Reverse and apply multiplier
        Vector2 newVelocity = incomingVelocity * multiplier;

        rb.linearVelocity = newVelocity;

        Debug.Log($"🌀 Tilemap bounce: pre-collision = {incomingVelocity}, new = {newVelocity}", this);
    }
}
