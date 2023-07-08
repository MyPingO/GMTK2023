using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    private Vector3 offset;  // Distance between the player and the camera

    void Start()
    {
        // Calculate and store the offset value by getting the distance between the player's position and camera's position
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
