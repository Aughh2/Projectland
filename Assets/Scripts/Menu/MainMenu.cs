using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialText;
    private bool isTutorialVisible = false;

    public void PlayGame()
    {
        SceneManager.LoadScene("Arena");
    }

    public void ToggleTutorial()
    {
        isTutorialVisible = !isTutorialVisible;
        tutorialText.SetActive(isTutorialVisible);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
