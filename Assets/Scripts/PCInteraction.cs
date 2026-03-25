using UnityEngine;
using UnityEngine.InputSystem;

public class PCInteract : MonoBehaviour
{
    [SerializeField] GameObject useText;
    [SerializeField] Transform cameraFocusPoint;
    bool playerInRange = false;

    InputAction interactAction;

    void Start()
    {
        useText.SetActive(false);
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.Enable();
    }

    void Update()
    {
        if (playerInRange && interactAction.WasPressedThisFrame())
        {
            GameManager.Instance.ToggleFocus(cameraFocusPoint);

            if (useText != null)
                useText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
        if (!GameManager.Instance.IsFocused() && useText != null)
            useText.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
        if (useText != null)
            useText.SetActive(false);
    }

    void OnDisable() => interactAction.Disable();
}