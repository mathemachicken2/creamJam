using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManagerBox : MonoBehaviour
{
    public static GameManagerBox Instance;

    [Header("UI References")]
    public GameObject imagePanel;
    public Image displayImage;

    [Header("Highlight Material")]
    public Material highlightMaterial;

    [Header("Zoom / Interaction Reference")]
    public InteractionMode interactionMode; // assign in inspector

    private List<HoverHighlight> interactives = new List<HoverHighlight>();
    public List<Image> sequenceImages = new List<Image>();

    public float delayBetweenImages = 0.5f;
    public float fadeDuration = 0.5f;
    public GameObject playSequenceButton;

    void Awake()
    {
       InitializeSequenceImages();

        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        if (imagePanel != null) imagePanel.SetActive(false);
        if (displayImage == null)
            Debug.LogWarning("GameManagerBox: displayImage not assigned!");
        if (highlightMaterial == null)
            Debug.LogWarning("GameManagerBox: highlightMaterial not assigned!");
        if (interactionMode == null)
            Debug.LogWarning("GameManagerBox: InteractionMode not assigned in inspector!");
    }

    void Update()
    {
        if (imagePanel != null && imagePanel.activeSelf)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ExitUIPanelMode();
            }
        }
    }

    public void PlayImageSequence(GameObject clickedPrefab)
    {
        if (clickedPrefab != null && clickedPrefab.CompareTag("CreamPrefab"))
        {
            StartCoroutine(ImageSequenceRoutine());
        }
    }
    private void InitializeSequenceImages()
    {
        foreach (var img in sequenceImages)
        {
            if (img != null)
            {
                Color c = img.color;
                c.a = 0f; // fully invisible
                img.color = c;

                img.gameObject.SetActive(true); // ensure it's active
            }
        }
    }
    private IEnumerator ImageSequenceRoutine()
    {
        // Hide panel immediately when button is pressed
        HidePrefabUI();

        // Make sure all images start invisible
        foreach (var img in sequenceImages)
        {
            if (img != null)
            {
                Color c = img.color;
                c.a = 0f;
                img.color = c;
                img.gameObject.SetActive(true);
            }
        }

        // Show images one by one
        foreach (var img in sequenceImages)
        {
            if (img != null)
            {
                yield return new WaitForSeconds(delayBetweenImages);

                // Fade in
                yield return StartCoroutine(FadeImage(img, 0f, 1f));
                AudioManager.Instance.Play("ApplyCream");
            }
        }

        
        foreach (var img in sequenceImages)
        {
            if (img != null)
            {
                StartCoroutine(FadeImage(img, 1f, 0f));
            }
        }
    }
    private IEnumerator FadeImage(Image img, float startAlpha, float endAlpha)
    {
        float time = 0f;

        Color c = img.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            img.color = c;

            yield return null;
        }

        c.a = endAlpha;
        img.color = c;
    }
    public void RegisterInteractive(HoverHighlight interactive)
    {
        if (interactive == null) return;
        if (!interactives.Contains(interactive))
            interactives.Add(interactive);

        if (highlightMaterial != null)
            interactive.highlightMaterial = highlightMaterial;
    }

    public bool IsUIOpen()
    {
        return imagePanel != null && imagePanel.activeSelf;
    }

    public void ShowPrefabUI(Sprite objectSprite, GameObject clickedPrefab)
    {
        if (imagePanel != null && displayImage != null)
        {
            imagePanel.SetActive(true);
            displayImage.sprite = objectSprite;

            // Freeze gameplay & unlock cursor
            EnterUIPanelMode();

            // Enable or disable PlayImageSequence button based on tag
            if (playSequenceButton != null)
                playSequenceButton.SetActive(clickedPrefab.CompareTag("CreamPrefab"));
        }
    }

    public void HidePrefabUI()
    {
        if (imagePanel != null)
            imagePanel.SetActive(false);
    }

    public void EnterUIPanelMode()
    {
        if (interactionMode != null)
            interactionMode.EnterInteraction();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitUIPanelMode()
    {
        HidePrefabUI();

        if (interactionMode != null)
            interactionMode.ExitInteraction();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}