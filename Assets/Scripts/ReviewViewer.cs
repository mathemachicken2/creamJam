using UnityEngine;
using UnityEngine.UI;

public class ReviewViewer : MonoBehaviour
{
    public GameObject viewerPanel;
    public Image displayImage;

    [Header("All review images")]
    private Sprite[] images;

    public GameObject[] buttonsToEnable; // all buttons to re-enable on close

    private int currentIndex = 0;

    public void OpenViewer(Sprite[] newImages)
    {
        if (newImages == null || newImages.Length == 0) return;

        images = newImages;
        currentIndex = 0;

        viewerPanel.SetActive(true);
        ShowImage();
    }

    public void CloseViewer()
    {
        viewerPanel.SetActive(false);
        viewerPanel.SetActive(false);

        // Re-enable buttons when closing
        if (buttonsToEnable != null)
        {
            foreach (var btn in buttonsToEnable)
                btn.gameObject.SetActive(true);
        }
    }

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % images.Length;
        ShowImage();
    }

    public void PrevImage()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = images.Length - 1;

        ShowImage();
    }

    public void SetImages(Sprite[] newImages)
    {
        images = newImages;
        currentIndex = 0;
        ShowImage();
    }
    void ShowImage()
    {
        displayImage.sprite = images[currentIndex];
    }
}