using UnityEngine;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject coinObject; // ref to the coin object
    [SerializeField] GameObject projectile; // ref to projectile object

    [SerializeField] Transform spawnPoint; // ref point for projectile
    [SerializeField] float fireStrength = 5f; // projectile fire

    // Stores the current dino object the player has detected
    Collectable currentDinosaur = null;
    // Flag to check if the player can interact with objects
    bool canInteract = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Player's current score
    int currentScore = 0;
    // Flag to check if the player can interact with objects

    // The Interact callback for the Interact Input Action
    // This method is called when the player presses the interact button

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("collectable"))
        {
            // Set the canInteract flag to true
            // Get the CoinBehaviour component from the detected object
            Debug.Log(other.gameObject.name + "Dinosaur");
            canInteract = true;
            currentDinosaur = other.GetComponent<Collectable>();
        }
    }
    void OnInteract()
    {
        // Check if the player can interact with objects
        if (canInteract)
        {
            if (currentDinosaur != null)
            {
                Debug.Log("Dinosaur retrieved");
                // Call the Collect method on the coin object
                // Pass the player object as an argument
                currentDinosaur.Collect(this);
            }
        }
    }

    public void ModifyScore(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentScore += amt;

        // Update UIManager to reflect the new score
        UIManager.instance.ModifyScore(currentScore);
    }

    void OnFire()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        Vector3 fireForce = spawnPoint.forward * fireStrength;
        newProjectile.GetComponent<Rigidbody>().AddForce(fireForce);
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
    