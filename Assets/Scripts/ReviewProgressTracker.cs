using UnityEngine;

public class ReviewProgressTracker : MonoBehaviour
{
    public static ReviewProgressTracker Instance;

    private int buttonsCompleted = 0;
    private int totalButtons = 3; // set to how many buttons/lists exist

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ButtonCompleted()
    {
        buttonsCompleted = Mathf.Min(buttonsCompleted + 1, totalButtons);
    }

    public bool AllButtonsCompleted()
    {
        return buttonsCompleted >= totalButtons;
    }
}