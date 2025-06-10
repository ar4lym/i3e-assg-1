using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject coinObject; // ref to the coin object
    [SerializeField] GameObject projectile; // ref to projectile object

    [SerializeField] Transform spawnPoint; // ref point for projectile
    [SerializeField] float fireStrength = 5f; // projectile fire

     // Stores the current coin object the player has detected
    collectable currentDinosaur = null;
    // Flag to check if the player can interact with objects
    bool canInteract = false;
    DoorBehaviour currentDoor = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Player's current score
    int currentScore = 0;
    // Flag to check if the player can interact with objects
    collectable currentcollectable = null;

    // The Interact callback for the Interact Input Action
    // This method is called when the player presses the interact button


    void OnInteract()
    {
        // Check if the player can interact with objects
        if (canInteract)
        {
            if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                currentDoor.Interact();

                // Check if the player can interact with objects
            }

            // Check if the player has detected a coin or a door
            if (currentcollectable != null)
            {
                Debug.Log("Retrieving Dinosaur");
                // Call the Collect method on the coin object
                // Pass the player object as an argument
                currentcollectable.Collect(this);
            }
        }
    }

     public void ModifyScore(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentScore += amt;
    }


    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * fireStrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name);

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.gameObject.GetComponent<DoorBehaviour>();
        }
        // Check if the player detects a trigger collider tagged as "Collectible" or "Door"
        if (other.CompareTag("collectable"))
        {
            // Set the canInteract flag to true
            // Get the CoinBehaviour component from the detected object
            canInteract = true;
            currentDinosaur = other.GetComponent<collectable>();
        }
    }

    // Trigger Callback for when the player exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected coin or door
        if (currentDinosaur != null)
        {
            // If the object that exited the trigger is the same as the current coin
            if (other.gameObject == currentDinosaur.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current coin to null
                // This prevents the player from interacting with the coin
                canInteract = false;
                currentDinosaur = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // collect dinoaur

}