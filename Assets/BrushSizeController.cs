using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BrushSizeController : MonoBehaviour
{
    [Header("Brush References")]
    public Transform brushTip;
    public Renderer brushRenderer;

    [Header("VR Controller Haptics")]
    public ActionBasedController rightController;
    [Range(0f, 1f)] public float hapticIntensity = 0.3f;
    public float hapticDuration = 0.05f;

    [Header("Size Settings")]
    public LayerMask canvasLayer;
    // Automatically initialized to 0.2f as requested
    public float maxDistance = 0.2f;
    public float minSize = 0.005f;
    public float maxSize = 0.05f;

    private bool isNearCanvas = false;

    void Update()
    {
        float currentSize = minSize;
        Vector3 rayOrigin = brushTip.position - (brushTip.forward * 0.05f);
        float rayLength = maxDistance + 0.1f;

        if (Physics.Raycast(rayOrigin, brushTip.forward, out RaycastHit hit, rayLength, canvasLayer))
        {
            float distanceToCanvas = Vector3.Distance(brushTip.position, hit.point);

            // Trigger haptic pulse ONLY on real physical contact (distance <= 1cm)
            if (distanceToCanvas <= 0.06f && !isNearCanvas)
            {
                TriggerHapticPulse();
                isNearCanvas = true;
            }

            // Dynamically scale the brush size within the 0.2f boundary area
            if (distanceToCanvas < maxDistance)
            {
                float t = 1f - (distanceToCanvas / maxDistance);
                t = Mathf.Clamp01(t);
                currentSize = Mathf.Lerp(minSize, maxSize, t);
            }

            // Reset haptic lock as soon as the brush pulls back from the contact boundary
            if (distanceToCanvas > 0.06f)
            {
                isNearCanvas = false;
            }
        }
        else
        {
            // Reset haptic lock if the raycast completely loses sight of the canvas
            isNearCanvas = false;
        }

        brushTip.localScale = new Vector3(currentSize, currentSize, currentSize);
    }

    private void TriggerHapticPulse()
    {
        if (rightController != null)
        {
            rightController.SendHapticImpulse(hapticIntensity, hapticDuration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaletteColor"))
        {
            Renderer paletteRenderer = other.GetComponent<Renderer>();

            if (paletteRenderer != null && brushRenderer != null)
            {
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