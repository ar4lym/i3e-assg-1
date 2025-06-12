using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
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
    public int maxHealth = 100;
    public int currentHealth = 100; 
    // Player's health
    public Transform respawnPoint; 

    public Animator doorAnimation;
    public bool isCollected;
    public bool redCollected;

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
   
    void OnInteract()
    {
        // Check if the player can interact with objects
        if (canInteract)
        {
            if (currentDinosaur != null)
            {
                Debug.Log("Dinosaur retrieved");
                // Call the Collect method on the dinosaur object
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

    

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateHealthUI(currentHealth); // ✅ Update UI
        }
        else
        {
            Debug.LogError("UIManager instance is null! Cannot update health UI.");
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player has died!");
            InstantRespawn(); // ✅ Respawn player
        }
    }


    public void ModifyHealth(int amount)
    {
        // Check if the current health is less than the maximum health
        // If it is, increase the current health by the amount passed as an argument
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            // Check if the current health exceeds the maximum health
            // If it does, set the current health to the maximum health
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    public void InstantDeath()
    {
        Debug.Log("Player has died instantly!");
        currentHealth = 0; // ✅ Set health to 0
        UIManager.instance.UpdateHealthUI(currentHealth); // ✅ Update UI

        InstantRespawn(); // ✅ Respawn player immediately
    }

    public void InstantRespawn()
    {
        Debug.Log("Respawning player...");
        currentHealth = maxHealth; // ✅ Restore health
        UIManager.instance.UpdateHealthUI(currentHealth);
        transform.position = respawnPoint.position; // ✅ Move player to respawn point
    }


   

    // raycast
    private PlayerInput playerInput; // Reference to PlayerInput
    private InputAction raycastAction; // Reference to Raycast action

    [SerializeField] private float raycastRange = 5f;
    [SerializeField] private LayerMask collectableLayer;

    private LineRenderer lineRenderer;

    void Start()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Start and end points
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Simple visible shader
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); // Get PlayerInput component
        if (playerInput != null)
        {
            raycastAction = playerInput.actions["Raycast"]; // Get the "Raycast" action
        }
        else
        {
            Debug.LogError("PlayerInput component is missing!");
        }
        isCollected = false;
        redCollected = false;
    }

    void OnEnable()
    {
        if (raycastAction != null)
        {
            raycastAction.Enable();
            raycastAction.performed += ctx => PerformRaycast();
        }
    }

    void OnDisable()
    {
        if (raycastAction != null)
        {
            raycastAction.Disable();
        }
    }

    void PerformRaycast()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        RaycastHit hit;
        Debug.DrawRay(rayOrigin, rayDirection * raycastRange, Color.red, 2f);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastRange, collectableLayer))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            Collectable collectable = hit.collider.GetComponent<Collectable>();
            if (collectable != null)
            {
                collectable.Highlight(); // Apply highlight effect first
                Debug.Log("Dinosaur collected!");
                collectable.Collect(this);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

}
