using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public static HealthUI instance;
    public TextMeshProUGUI Health;
    

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

    }

    void Start()
    {
        SetInitialHealth(100); // ✅ Set health to 100 when the game starts
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