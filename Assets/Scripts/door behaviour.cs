using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public Animator doorAnimation;
    public bool isCollected;
    public bool redCollected;

    void OnTriggerEnter(Collider other)
    {
        // purple
        if (other.gameObject.tag == "doorCollider")
        {
            isCollected = true;
            doorAnimation.SetBool("isCollected", isCollected);
        }
        if (other.gameObject.tag == "Closedoor")
        {
            isCollected = false;
            doorAnimation.SetBool("isCollected", isCollected);
        }
        
        // red
        if (other.gameObject.tag == "redoorCollider")
        {
            bool redCollected = true;
            doorAnimation.SetBool("redCollected", redCollected);
        }
        if (other.gameObject.tag == "Closedoor")
        {
            redCollected = false;
            doorAnimation.SetBool("redCollected", redCollected);
        }

    }
    void Awake()
    {
        
        isCollected = false;
        redCollected = false;
    }
}