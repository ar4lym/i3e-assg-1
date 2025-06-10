using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public void Interact()
    {
        Vector3 DoorRotation = transform.rotation.eulerAngles;
        if (DoorRotation.y == 0f)
        {
            // If the door is closed, open it
            DoorRotation.y += 90f; // Set to 90 degrees to open the door
            transform.eulerAngles = DoorRotation;
        }
        else 
        {
            
            DoorRotation.y = 0f; 
            transform.eulerAngles = DoorRotation;
        }
    }
}