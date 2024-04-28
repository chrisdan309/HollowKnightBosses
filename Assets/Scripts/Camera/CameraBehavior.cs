using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;
    public float smoothTime = 0.3f;

    private Vector3 _velocity = Vector3.zero;

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            
            targetPosition.x = Mathf.Clamp(targetPosition.x, minCameraPos.x, maxCameraPos.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minCameraPos.y, maxCameraPos.y);
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }
}