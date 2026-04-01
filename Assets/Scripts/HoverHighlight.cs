using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    [Header("Highlight")]
    public Material highlightMaterial;   // red glow
    [HideInInspector] public Material originalMaterial;
    [HideInInspector] public Renderer rend;

    [Header("Sprite")]
    public Sprite objectSprite;          // prefab-specific sprite

    void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalMaterial = rend.material;
        else
            Debug.LogWarning($"{name}: HoverHighlight requires a Renderer!");
    }

    void Start()
    {
        // Register with GameManager so it can assign highlightMaterial if needed
        if (GameManagerBox.Instance != null)
            GameManagerBox.Instance.RegisterInteractive(this);
    }

    public void SetHighlight()
    {
        if (rend != null && highlightMaterial != null)
            rend.material = highlightMaterial;
    }

    public void ResetMaterial()
    {
        if (rend != null)
            rend.material = originalMaterial;
    }
}