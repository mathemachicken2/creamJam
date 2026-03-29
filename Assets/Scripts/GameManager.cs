using UnityEngine;
using UnityEngine.InputSystem;

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

    InputAction exitAction;

    void Awake()
    {

        exitAction = new InputAction(binding: "<Keyboard>/escape");
        exitAction.AddBinding("<Keyboard>/s");
        exitAction.Enable();
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

        if (isFocused && exitAction.WasPressedThisFrame())
        {
            ToggleFocus(null);
        }

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
        if (isFocused)
        {
            // Zoom out
            isFocused = false;
            focusPoint = null;

            if (playerMovement != null) playerMovement.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // Zoom in
            focusPoint = newFocusPoint;
            isFocused = true;

            if (playerMovement != null) playerMovement.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public bool IsFocused()
    {
        return isFocused;
    }
}