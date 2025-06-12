using UnityEngine;

public class RedCard : MonoBehaviour
{
    public GameObject redoorCollider;

    void Start()
    {
        if (redoorCollider != null)
        {
            redoorCollider.SetActive(false);
        }
        else
        {
            Debug.LogError("⚠️ redoorCollider is NOT assigned! Please assign it in the Inspector.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the card trigger: " + other.gameObject.name); // ✅ Debugging
        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Player collected the card!");
            redoorCollider.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("⚠️ Non-player object entered the trigger: " + other.gameObject.name);
        }
    }
}