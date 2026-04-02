using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionMode : MonoBehaviour
{
    public MonoBehaviour cameraController;
    private bool isInteracting = false;

    void Update()
    {
        if (!isInteracting) return;

        // Exit zoom / interaction with S only
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            Debug.Log("Exiting interaction mode via S key.");
            ExitInteraction();
        }
    }

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