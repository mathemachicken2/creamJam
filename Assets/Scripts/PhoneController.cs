using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneUI : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform phoneBase;      // PhoneBaseImage
    public GameObject revealImage;        // App panel
    public Button openAppButton;          // Button inside phone
    
    public GameObject tabPromptPanel;
    private InputAction escapeAction;

    [Header("Slide Settings")]
    public float slideSpeed = 8f;
    public float visibleY = 120f;

    private Vector2 hiddenPos;
    private Vector2 showPos;
    private bool isVisible = false;
    private bool isInitialized = false;

    private InputAction tabAction;
    public ReviewViewer reviewViewer; // drag your ReviewViewer here

    private bool hasShownPrompt = false;

    public Image blackoutImage;
    public GameObject finalImage;

    void Start()
    {
        // Input system
        tabAction = new InputAction(binding: "<Keyboard>/tab");
        tabAction.Enable();

        escapeAction = new InputAction(binding: "<Keyboard>/escape");
        escapeAction.Enable();

        // App panel off
        revealImage.SetActive(false);
        
        tabPromptPanel.SetActive(false);

        // Button click
        openAppButton.onClick.AddListener(OpenApp);

        // Initialize positions if phoneBase is active
        if (phoneBase.gameObject.activeSelf)
            InitializePhone();
    }

    void Update()
    {
        if (ReviewProgressTracker.Instance.AllButtonsCompleted() && !hasShownPrompt)
        {
            tabPromptPanel.SetActive(true);
            
            hasShownPrompt = true;
        }
        // Only show prompt and allow Tab if all buttons/lists are done
        

        if (escapeAction.WasPressedThisFrame())
        {
            if (tabPromptPanel.activeSelf)
            {
                tabPromptPanel.SetActive(false);
                
                return; // prevent ESC from affecting other things this frame
            }
        }

        if (tabAction.WasPressedThisFrame())
        {
            if (!ReviewProgressTracker.Instance.AllButtonsCompleted())
                return;

            if (!isInitialized)
            {
                phoneBase.gameObject.SetActive(true);
                InitializePhone();
            }

            isVisible = !isVisible;

            if (isVisible)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Hide prompt when opening phone
                if (tabPromptPanel.activeSelf)
                    tabPromptPanel.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                revealImage.SetActive(false);
            }
        }

        // Smooth slide
        if (isInitialized)
        {
            Vector2 targetPos = isVisible ? showPos : hiddenPos;
            phoneBase.anchoredPosition = Vector2.Lerp(phoneBase.anchoredPosition, targetPos, Time.deltaTime * slideSpeed);
        }
    }

    private System.Collections.IEnumerator EndSequence()
    {
        yield return new WaitForSeconds(4f);

        yield return StartCoroutine(FadeBlack(0f, 1f, 2f));

        yield return new WaitForSeconds(3f);

        finalImage.SetActive(true);

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }

    private System.Collections.IEnumerator FadeBlack(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = blackoutImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            blackoutImage.color = color;

            yield return null;
        }

        color.a = endAlpha;
        blackoutImage.color = color;
    }
    private void InitializePhone()
    {
        AudioManager.Instance.Play("OpenPhone");
        hiddenPos = new Vector2(phoneBase.anchoredPosition.x, -phoneBase.rect.height);
        showPos = new Vector2(phoneBase.anchoredPosition.x, visibleY);

        phoneBase.anchoredPosition = hiddenPos;
        isInitialized = true;
    }

    private void OpenApp()
    {
        AudioManager.Instance.Play("Click");
        revealImage.SetActive(true);
        StartCoroutine(EndSequence());
    }

    private void OnDisable()
    {
        tabAction.Disable();
        escapeAction.Disable();
    }
}