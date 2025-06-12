using UnityEngine;

public class CardScript : MonoBehaviour
{
    public GameObject doorCollider;

    void Start()
    {
        if (doorCollider != null)
        {
            doorCollider.SetActive(false);
        }
        else
        {
            Debug.LogError("⚠️ doorCollider is NOT assigned! Please assign it in the Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the card trigger: " + other.gameObject.name); // ✅ Debugging

        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Player collected the card!");
            doorCollider.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("⚠️ Non-player object entered the trigger: " + other.gameObject.name);
        }
    }
}