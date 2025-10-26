using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player;   // The player to follow

    [Header("Camera Follow Settings")]
    public float smoothSpeed = 5f; // How smoothly the camera follows the player

    [Header("Y-Axis Limits")]
    public float minY = -5f;   // Lowest point the camera can go
    public float maxY = 10f;   // Highest point the camera can go

    private float fixedX; // Keeps the X position constant

    void Start()
    {
        // Remember the camera's initial X position
        fixedX = transform.position.x;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Smooth follow on Y
        float targetY = Mathf.Lerp(transform.position.y, player.position.y, smoothSpeed * Time.deltaTime);

        // Clamp camera's Y position within bounds
        targetY = Mathf.Clamp(targetY, minY, maxY);

        // Apply the position (only Y changes)
        transform.position = new Vector3(fixedX, targetY, transform.position.z);
    }
}
