using UnityEngine;
using System.Collections;

public class RecoveryBehaviour : MonoBehaviour
{
    [SerializeField] int healAmount = 7; // ✅ Amount of health to recover
    private bool playerInside = false; // ✅ Track if player is inside
    private PlayerBehaviour player; // ✅ Reference to the player script
    private Coroutine healingCoroutine; // ✅ Store the running coroutine

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ✅ Ensure the player is detected
        {
            player = other.GetComponent<PlayerBehaviour>();

            if (player != null)
            {
                playerInside = true;

                // ✅ Stop any existing coroutine before starting a new one
                if (healingCoroutine != null)
                {
                    StopCoroutine(healingCoroutine);
                }

                healingCoroutine = StartCoroutine(HealPlayerOverTime());
            }
            else
            {
                Debug.LogError("⚠️ PlayerBehaviour script is missing on Player!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Player exited recovery area!");
            playerInside = false;

            // ✅ Stop the healing coroutine when player leaves
            if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine);
                healingCoroutine = null;
            }
        }
    }

    private IEnumerator HealPlayerOverTime()
    {
        while (playerInside)
        {
            if (player.currentHealth < player.maxHealth) // ✅ Heal only if damaged
            {
                Debug.Log("✅ Healing player...");
                player.ModifyHealth(healAmount);
            }
            else
            {
                Debug.Log("⚠️ Player is already at full health! No healing needed.");
            }

            yield return new WaitForSeconds(1f); // ✅ Heal every second
        }
    }
}
