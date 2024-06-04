using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    // Public variables to set in the Unity Inspector
    public Transform targetDestination; // The target position to teleport the player to

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the Player tag
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the target destination
            other.transform.position = targetDestination.position;
            Debug.Log("Player teleported to target destination.");
        }
    }
}
