using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Camera Settings")]
    public Transform playerCamera;      // assign your Player camera
    public float focusSpeed = 2f;       // speed of camera zoom

    [Header("Player References")]
    public PlayerMovement playerMovement; // assign your PlayerMovement script

    private Vector3 originalLocalPos;
    private Quaternion originalLocalRot;

    private Transform focusPoint;       // current zoom target
    private bool isFocused = false;

    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;

        originalLocalPos = playerCamera.localPosition;
        originalLocalRot = playerCamera.localRotation;
    }

    void Update()
    {
        if (focusPoint != null && isFocused)
        {
            // WORLD SPACE LERP
            playerCamera.position = Vector3.Lerp(playerCamera.position, focusPoint.position, Time.deltaTime * focusSpeed);
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, focusPoint.rotation, Time.deltaTime * focusSpeed);
        }
        else
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, originalLocalPos, Time.deltaTime * focusSpeed);
            playerCamera.localRotation = Quaternion.Slerp(playerCamera.localRotation, originalLocalRot, Time.deltaTime * focusSpeed);
        }
    }

    /// <summary>
    /// Toggle focus to a point (zoom in/out)
    /// </summary>
    /// <param name="newFocusPoint"></param>
    public void ToggleFocus(Transform newFocusPoint)
    {
        if (isFocused && focusPoint == newFocusPoint)
        {
            // Zoom out
            isFocused = false;
            focusPoint = null;

            // Restore player control
            if (playerMovement != null) playerMovement.enabled = true;

            // Lock cursor again
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // Zoom in
            focusPoint = newFocusPoint;
            isFocused = true;

            // Freeze player
            if (playerMovement != null) playerMovement.enabled = false;

            // Unlock cursor for UI interaction
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public bool IsFocused()
    {
        return isFocused;
    }
}