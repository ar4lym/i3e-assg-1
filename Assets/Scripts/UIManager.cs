using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text Health;
    public TMP_Text dinoretrieved;

    public int currentScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        

        if (Health == null)
        {
            Debug.LogError("Health UI Text is missing! Please assign it in the Inspector.");
        }

        if (dinoretrieved == null)
        {
            Debug.LogError("Dinosaurs Retrieved UI Text is missing! Please assign it in the Inspector.");
        }
    }

    void Start()
    {
        dinoretrieved.text = "Dinosaurs retrieved: " + currentScore.ToString();
        SetInitialHealth(100); // ✅ Set health to 100 when the game starts
    }

    public void ModifyScore(int v)
    {
        currentScore++;
        dinoretrieved.text = "Dinosaurs retrieved: " + currentScore.ToString();
    }

    public void SetInitialHealth(int health)
    {
        if (Health != null)
        {
            Health.text = "Health: " + health.ToString(); // ✅ Display 100 at the start
        }
    }

    public void UpdateHealthUI(int health)
    {
        if (Health != null)
        {
            Health.text = "Health: " + health.ToString(); // ✅ Update UI when health changes
        }
        else
        {
            Debug.LogError("Health UI Text is missing!");
        }
    }
}