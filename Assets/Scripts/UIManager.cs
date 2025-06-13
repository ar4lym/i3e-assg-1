using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public TextMeshProUGUI dinoretrieved;

    public int currentScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        


        if (dinoretrieved == null)
        {
            Debug.LogError("Dinosaurs Retrieved UI Text is missing! Please assign it in the Inspector.");
        }
    }

    void Start()
    {
        dinoretrieved.text = "Dinosaurs retrieved: " + currentScore.ToString() + "/15";
        
    }

    public void ModifyScore(int v)
    {
        currentScore++;
        dinoretrieved.text = "Dinosaurs retrieved: " + currentScore.ToString() + "/15";

        if (currentScore == 15)
        {
            Debug.Log("✅ All collectibles gathered! Loading end scene...");
            SceneManager.LoadScene(3); // Replace "EndScene" with your actual scene name
        }
    }

 
}

    