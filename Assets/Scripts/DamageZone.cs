using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damagePerSecond = 5; // ✅ Correct damage amount
    private bool playerInside = false;
    private PlayerBehaviour player; // ✅ Reference to the player script
    private Coroutine damageCoroutine; // ✅ Store the running coroutine

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ✅ Ensure the player is detected
        {
            Debug.Log("Player entered damage zone!");
            player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                playerInside = true;

                // ✅ Stop any existing coroutine before starting a new one
                if (damageCoroutine != null)
                {
                    StopCoroutine(damageCoroutine);
                }

                damageCoroutine = StartCoroutine(ApplyDamage());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited damage zone!");
            playerInside = false;

            // ✅ Stop the damage coroutine when player leaves
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private System.Collections.IEnumerator ApplyDamage()
    {
        while (playerInside)
        {
            Debug.Log("Applying damage to player...");
            player.TakeDamage(damagePerSecond);

            // ✅ Check if player health reaches 0
            if (player.currentHealth <= 0)
            {
                Debug.Log("Player has died!");
                player.InstantRespawn(); // ✅ Respawn player immediately
                yield break; // ✅ Stop coroutine after respawning
            }

            yield return new WaitForSeconds(1f); // ✅ Apply damage every second
        }
    }
}