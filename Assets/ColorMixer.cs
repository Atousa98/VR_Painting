using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    private Renderer mixerRenderer;

    private bool isFirstTouch = true;

    void Start()
    {
        mixerRenderer = GetComponent<Renderer>();
    }

    public void MixWithBrush(Renderer brushRenderer)
    {
        Color brushColor = brushRenderer.material.color;

        if (isFirstTouch)
        {
            mixerRenderer.material.color = brushColor;
            isFirstTouch = false;
        }
        else
        {
            Color currentColor = mixerRenderer.material.color;
            Color newMixedColor = Color.Lerp(currentColor, brushColor, 0.5f);

            mixerRenderer.material.color = newMixedColor;

            brushRenderer.material.color = newMixedColor;
        }
    }
}