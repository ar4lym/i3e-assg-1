using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton ()
    {
        SceneManager.LoadScene(1);
    }
    public void OnInstructionButton ()
    {
        SceneManager.LoadScene(2);
    }
    public void OnBackButton ()
    {
        SceneManager.LoadScene(0);
    }
}
