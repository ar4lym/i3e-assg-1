using UnityEngine;

public class InstantKill : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ✅ Ensure the player is detected
        {
            Debug.Log("Player touched the instant kill zone! Instant death.");
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.InstantDeath(); // ✅ Call instant death method
            }
        }
    }
}
