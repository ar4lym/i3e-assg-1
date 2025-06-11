using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 5; // Damage applied per second
    private bool playerInside = false;
    private PlayerBehaviour player; // Reference to the player script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ✅ Ensure the player is detected
        {
            Debug.Log("Player entered damage zone!"); // ✅ Debugging
            player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                playerInside = true;
                StartCoroutine(ApplyDamage());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited damage zone!"); // ✅ Debugging
            playerInside = false;
            StopCoroutine(ApplyDamage());
        }
    }

    private System.Collections.IEnumerator ApplyDamage()
    {
        while (playerInside)
        {
            Debug.Log("Applying damage to player..."); // ✅ Debugging
            player.TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1f); // Apply damage every second
        }
    }
}