using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter(Collision other);
    if (other.CompareTag("Untagged"))
    {
       Destroy(gameObject);
    }
}
