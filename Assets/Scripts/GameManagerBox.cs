using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBox : MonoBehaviour
{
    public static GameManagerBox Instance;

    [Header("UI References")]
    public GameObject imagePanel;   // inactive by default
    public Image displayImage;

    [Header("Highlight Material")]
    public Material highlightMaterial;

    private List<HoverHighlight> interactives = new List<HoverHighlight>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        if (imagePanel != null) imagePanel.SetActive(false);
        if (displayImage == null)
            Debug.LogWarning("GameManagerBox: displayImage not assigned!");
        if (highlightMaterial == null)
            Debug.LogWarning("GameManagerBox: highlightMaterial not assigned!");
    }

    public void RegisterInteractive(HoverHighlight interactive)
    {
        if (interactive == null) return;

        if (!interactives.Contains(interactive))
            interactives.Add(interactive);

        if (highlightMaterial != null)
            interactive.highlightMaterial = highlightMaterial;
    }

    public void ShowPrefabUI(Sprite objectSprite)
    {
        if (imagePanel != null && displayImage != null)
        {
            imagePanel.SetActive(true);
            displayImage.sprite = objectSprite;
        }
    }

    public void HidePrefabUI()
    {
        if (imagePanel != null)
            imagePanel.SetActive(false);
    }
}