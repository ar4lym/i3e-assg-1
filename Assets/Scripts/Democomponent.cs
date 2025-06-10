using UnityEngine;

public class Democomponent : MonoBehaviour
{
    string message = "";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i < 11; i++)
        {
            message += i + " ";
        }
        Debug.Log(message);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("HAHA");
    }

}