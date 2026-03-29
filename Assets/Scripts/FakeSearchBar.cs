using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class FakeSearchBar : MonoBehaviour
{
    public TMP_Text searchText; // TMP instead of UI Text

    private string targetText = "CreamBeam.com";
    private int currentIndex = 0;

    private InputAction anyKeyAction;
    private bool isTyping = false;

    void Start()
    {
        searchText.text = "";

        // Detect ANY key press
        anyKeyAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/anyKey");
        anyKeyAction.Enable();
    }

    void Update()
    {
        if (!GameManager.Instance.IsFocused()) return;

        if (Keyboard.current == null) return;

        foreach (var key in Keyboard.current.allKeys)
        {
            if (key.wasPressedThisFrame)
            {
                TypeNextLetter();
                break;
            }
        }
    }
    void TypeNextLetter()
    {
        if (currentIndex < targetText.Length)
        {
            searchText.text += targetText[currentIndex];
            currentIndex++;
        }
    }
    IEnumerator TypeSequence()
    {
        isTyping = true;

        while (currentIndex < targetText.Length)
        {
            searchText.text += targetText[currentIndex];
            currentIndex++;
            Debug.Log($"Typed: {searchText.text}");
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    public bool IsComplete()
    {
        return currentIndex >= targetText.Length;
    }

    public void ResetSearch()
    {
        searchText.text = "";
        currentIndex = 0;
    }

    void OnDisable()
    {
        anyKeyAction.Disable();
    }
}