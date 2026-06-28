using UnityEngine;
using UnityEngine.InputSystem;

public class BrushSizeController : MonoBehaviour
{
    [Header("Brush References")]
    public Transform brushTip;
    public Renderer brushRenderer; // Added this to control the sphere's material color

    [Header("Size Settings")]
    public LayerMask canvasLayer;
    public float maxDistance = 0.1f;
    public float minSize = 0.005f;
    public float maxSize = 0.05f;

    void Update()
    {
        float currentSize = minSize;
        Vector3 rayOrigin = brushTip.position - (brushTip.forward * 0.05f);

        if (Physics.Raycast(rayOrigin, brushTip.forward, out RaycastHit hit, 0.3f, canvasLayer))
        {
            float distanceToCanvas = Vector3.Distance(brushTip.position, hit.point);

            if (distanceToCanvas < maxDistance)
            {
                float t = 1f - (distanceToCanvas / maxDistance);
                t = Mathf.Clamp01(t);
                currentSize = Mathf.Lerp(minSize, maxSize, t);
            }
        }

        brushTip.localScale = new Vector3(currentSize, currentSize, currentSize);
    }

    // This function detects collision with the palette colors and mixing area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaletteColor"))
        {
            Renderer paletteRenderer = other.GetComponent<Renderer>();

            if (paletteRenderer != null && brushRenderer != null)
            {
                // Copy the base color from the palette object to the brush tip
                // Using sharedMaterial to prevent the palette colors from disappearing
                brushRenderer.material.color = paletteRenderer.sharedMaterial.color;
            }
        }
        else if (other.CompareTag("MixingArea"))
        {
            ColorMixer mixer = other.GetComponent<ColorMixer>();

            if (mixer != null && brushRenderer != null)
            {
                mixer.MixWithBrush(brushRenderer);
            }
        }
    }
}