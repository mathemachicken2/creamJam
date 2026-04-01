using UnityEngine;

public class InteractionMode : MonoBehaviour
{
    [Header("References")]
    public MonoBehaviour cameraController;

    private bool isInteracting = false;

    void Update()
    {
        if (!isInteracting) return;

        // Exit interaction using same style (simple key check only when active)
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S))
        {
            ExitInteraction();
        }
    }

    // Call this from your EXISTING input logic (E key)
    public void EnterInteraction()
    {
        if (isInteracting) return;

        isInteracting = true;

        if (cameraController != null)
            cameraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitInteraction()
    {
        if (!isInteracting) return;

        isInteracting = false;

        if (cameraController != null)
            cameraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}