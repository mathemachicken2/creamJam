using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    // Play button
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Open options
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    // Back button
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }
}