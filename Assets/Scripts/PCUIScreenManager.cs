using UnityEngine;
using UnityEngine.UI;

public class PCUIScreenManager : MonoBehaviour
{
    [Header("Screen Images")]
    public GameObject screensaver;
    public GameObject mailWindow;
    public GameObject webBrowser;
    public GameObject creamyBeams;

    [Header("Main Buttons (only on screensaver)")]
    public GameObject button1;
    public GameObject button2;

    [Header("Back Buttons")]
    public GameObject backButtonMail;
    public GameObject backButtonWeb;
    public GameObject backButtonCreamy;
    public FakeSearchBar fakeSearchBar;
    public ReviewViewer reviewViewer;

    void Start()
    {
        creamyBeams.SetActive(false);
        ShowScreen(screensaver);
    }

    

    public void OpenMail()
    {
        ShowScreen(mailWindow);
    }

    public void OpenWeb()
    {
        ShowScreen(webBrowser);

        if (fakeSearchBar != null)
            fakeSearchBar.ResetSearch();
    }

    public void OpenCreamy()
    {
        if (fakeSearchBar != null && !fakeSearchBar.IsComplete())
        {
            return;
        }

        fakeSearchBar.ResetSearch();
        ShowScreen(creamyBeams);
        if (reviewViewer != null)
            reviewViewer.CloseViewer();
    }

    public void GoBack()
    {
        ShowScreen(screensaver);
    }

    // --- Core logic ---

    void ShowScreen(GameObject screen)
    {
        AudioManager.Instance.Play("Click");
        // Turn off all screens
        screensaver.SetActive(false);
        mailWindow.SetActive(false);
        webBrowser.SetActive(false);
        creamyBeams.SetActive(false);

        // Turn off all buttons first
        button1.SetActive(false);
        button2.SetActive(false);

        backButtonMail.SetActive(false);
        backButtonWeb.SetActive(false);
        backButtonCreamy.SetActive(false);

        // Activate selected screen
        if (screen != null)
            screen.SetActive(true);

        // Logic per screen
        if (screen == screensaver)
        {
            button1.SetActive(true);
            button2.SetActive(true);
        }
        else if (screen == mailWindow)
        {
            backButtonMail.SetActive(true);
        }
        else if (screen == webBrowser)
        {
            backButtonWeb.SetActive(true);
        }
        else if (screen == creamyBeams)
        {
            backButtonCreamy.SetActive(true);
        }
    }
}