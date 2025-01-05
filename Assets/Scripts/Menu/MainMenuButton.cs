using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu"); // Replace with your main menu scene name
    }
}
