using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged")) // ✅ Check if object has "Untagged" tag
        {
            Debug.Log("Bullet hit an untagged object! Destroying...");
            Destroy(gameObject); // ✅ Destroy the bullet
        }
    }
}
