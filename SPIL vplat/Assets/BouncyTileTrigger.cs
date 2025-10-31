using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BouncyTileTrigger : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length == 0) return;

        // Get player Rigidbody2D
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Check contact point
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3Int tilePos = tilemap.WorldToCell(contactPoint);

        // Check if it's a BouncyTile
        TileBase tile = tilemap.GetTile(tilePos);
        if (tile is BouncyTile bouncy)
        {
            // Bounce only when hitting from above
            if (collision.contacts[0].normal.y > 0.5f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bouncy.bounceForce);
            }
        }
    }
}
