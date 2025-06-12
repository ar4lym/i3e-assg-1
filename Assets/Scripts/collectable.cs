using UnityEngine;

public class Collectable : MonoBehaviour
{
    MeshRenderer myMeshRenderer;
    [SerializeField] Material highlightMaterial; // Material to highlight the dino
    [SerializeField] Material originalMaterial; // Default material for the dino
    [SerializeField] int DinosaurValue = 1; // Coin value that will be added to the player's score

    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = myMeshRenderer.material; // Store the original material
    }
    
    public void Highlight()
    {
        myMeshRenderer.material = highlightMaterial; // Change the material to highlight
    }

    public void Unhighlight()
    {
        myMeshRenderer.material = originalMaterial; // Change back to the original material
    }

    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Dinosaur retrieved!");

        // Add the coin value to the player's score
        player.ModifyScore(DinosaurValue);

        // Highlight the collectible before destruction
        Highlight();

        // Wait for a short duration before destroying the object
        Invoke(nameof(DestroyCollectible), 0.5f); 
    }

    void DestroyCollectible()
    {
        Destroy(gameObject);
    }
}
