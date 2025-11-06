using UnityEngine;

public class SpikeTilemap : MonoBehaviour
{
    [Header("Respawn position (kun X og Y bruges)")]
    public Vector2 respawnPosition = new Vector2(0, 0);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RespawnPlayer(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        Vector3 newPos = new Vector3(respawnPosition.x, respawnPosition.y, 0f);
        player.transform.position = newPos;

        // Nulstil bevægelse (valgfrit men anbefalet)
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
}
