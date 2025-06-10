using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour

{
    public static UIManager instance;

    public TMP_Text dinoretrieved;
    public int currentScore = 0;

    void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
    
}

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        dinoretrieved.text = "Dinosaurs retrieved: " + currentScore.ToString();
    }

    public void ModifyScore(int v)
    {
        currentScore ++;
        dinoretrieved.text = "Dinosaurs retrieved: " + currentScore.ToString();
    }

    }
    // Update is called once per frame
   