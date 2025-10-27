using UnityEngine;

public class CameraFollowXY : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player; // The player to follow

    [Header("Follow Settings")]
    public float smoothSpeed = 5f; // How smoothly the camera follows

    [Header("Camera Limits")]
    public bool limitX = true;
    public bool limitY = true;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 10f;

    private Vector3 targetPos;

    void LateUpdate()
    {
        if (player == null) return;

        // Start with player’s current position
        targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Apply axis limits if enabled
        if (limitX)
            targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        if (limitY)
            targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        // Smooth follow movement
        Vector3 smoothed = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothed;
    }
}
