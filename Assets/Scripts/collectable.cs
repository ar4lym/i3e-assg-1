using UnityEngine;

public class Collectable : MonoBehaviour
{
    MeshRenderer myMeshRenderer;
    
    [SerializeField] int DinosaurValue = 1; // Coin value that will be added to the player's score

    
    
    

    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Dinosaur retrieved!");

        // Add the coin value to the player's score
        player.ModifyScore(DinosaurValue);

        

        // Wait for a short duration before destroying the object
        Invoke(nameof(DestroyCollectible), 0.5f); 
    }

    void DestroyCollectible()
    {
        Destroy(gameObject);
    }
}
