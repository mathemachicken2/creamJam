using UnityEngine;
using UnityEngine.InputSystem;


public class PickUpBox : MonoBehaviour
{
   // public InteractionMode interactionMode;

    public GameObject particlePrefab;
    [SerializeField] GameObject particlePrefab1;
    public GameObject creamPrefab;
    public GameObject useText;

    [SerializeField] Transform cameraFocusPoint;

    private bool playerInRange = false;
    private bool pickedUp = false;

    InputAction interactAction;

    void Start()
    {
        interactAction = new InputAction(binding: "<Keyboard>/e");
        interactAction.Enable();
        useText.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !pickedUp && interactAction.WasPressedThisFrame())
        {
            pickedUp = true;
            useText.SetActive(false);
            GameManager.Instance.ToggleFocus(cameraFocusPoint);

            

            if (particlePrefab != null)
            {
                GameObject p = Instantiate(particlePrefab, transform.position, Quaternion.identity);
                Destroy(p, 3f);
            }
            {
                GameObject p1 = Instantiate(particlePrefab1, transform.position, Quaternion.identity);
                Destroy(p1, 3f);
            }



            if (creamPrefab != null)
                Instantiate(creamPrefab, transform.position, Quaternion.identity);

           
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
            if (!GameManager.Instance.IsFocused() && useText != null)
                {
                    useText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exited: " + other.name);

        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (useText != null)
            {
                useText.SetActive(false);
                Debug.Log("Text hidden!");
            }
        }
    }

    void OnDisable()
    {
        interactAction.Disable();
    }
}