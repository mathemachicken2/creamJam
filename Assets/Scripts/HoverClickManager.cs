using UnityEngine;
using UnityEngine.InputSystem;  // Required for the new Input System

public class HoverClickManager : MonoBehaviour
{
    private HoverHighlight currentHover;

    void Update()
    {
        if (Camera.main == null) return;

        // Get current mouse position using new Input System
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            HoverHighlight hover = hit.collider.GetComponent<HoverHighlight>();

            // Handle hover highlight
            if (hover != currentHover)
            {
                if (currentHover != null)
                    currentHover.ResetMaterial();

                currentHover = hover;

                if (currentHover != null)
                    currentHover.SetHighlight();
            }

            // Handle click
            if (hover != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (GameManagerBox.Instance != null && hover.objectSprite != null)
                    GameManagerBox.Instance.ShowPrefabUI(hover.objectSprite);
            }
        }
        else
        {
            // Reset highlight when not hovering
            if (currentHover != null)
                currentHover.ResetMaterial();

            currentHover = null;
        }
    }
}