using UnityEngine;

public class ReviewButton : MonoBehaviour
{
    public ReviewViewer reviewViewer;
    public Sprite[] images;             // THIS button's images
    public GameObject[] siblingButtons; // all buttons to disable

    public void Open()
    {
        // Disable all buttons while viewer is open
        if (siblingButtons != null)
        {
            foreach (var btn in siblingButtons)
                btn.SetActive(false);
        }

        // Open viewer with THIS button’s images
        reviewViewer.OpenViewer(images);
    }
}