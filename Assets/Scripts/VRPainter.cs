using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRPainter : MonoBehaviour
{
    [Header("References")]
    public VRPaintingCanvas paintingCanvas;
    public BrushSettings brushSettings;

    [Header("Ray Settings")]
    public float maxRayDistance = 5f;
    public LayerMask canvasLayerMask;

    [Header("Visuals")]
    public LineRenderer laserPointer;
    public Transform rayOrigin;

    private Vector2 lastUV;
    private bool wasPainting = false;
    private ActionBasedController xrController;

    void Awake()
    {
        xrController = GetComponent<ActionBasedController>();
    }

    void Update()
    {
        bool triggerPressed = false;

        if (xrController != null)
        {
            float triggerVal = xrController.selectAction.action.ReadValue<float>();
            triggerPressed = triggerVal > 0.1f;
        }

        PerformRaycast(triggerPressed);
    }

    void PerformRaycast(bool triggerPressed)
    {
        if (rayOrigin == null) return;

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;

        if (laserPointer != null)
        {
            laserPointer.SetPosition(0, ray.origin);
            laserPointer.SetPosition(1, ray.origin + ray.direction * maxRayDistance);
        }

        if (Physics.Raycast(ray, out hit, maxRayDistance, canvasLayerMask))
        {
            if (laserPointer != null)
                laserPointer.SetPosition(1, hit.point);

            VRPaintingCanvas canvas = hit.collider.GetComponent<VRPaintingCanvas>();
            if (canvas == null) { wasPainting = false; return; }

            if (triggerPressed)
            {
                Vector2 uv = hit.textureCoord;

                if (wasPainting)
                    DrawLine(canvas, lastUV, uv);
                else
                    canvas.Paint(uv, brushSettings.brushColor,
                                 brushSettings.brushSize, brushSettings.hardness);

                lastUV = uv;
                wasPainting = true;
            }
            else
            {
                wasPainting = false;
            }
        }
        else
        {
            wasPainting = false;
        }
    }

    void DrawLine(VRPaintingCanvas canvas, Vector2 from, Vector2 to)
    {
        float dist = Vector2.Distance(from, to);
        int steps = Mathf.Max(1, Mathf.CeilToInt(
            dist * canvas.textureWidth / (brushSettings.brushSize * 0.5f)));

        for (int i = 0; i <= steps; i++)
        {
            float t = (float)i / steps;
            canvas.Paint(Vector2.Lerp(from, to, t),
                         brushSettings.brushColor,
                         brushSettings.brushSize,
                         brushSettings.hardness);
        }
    }

    public void SetColor(Color c) => brushSettings.brushColor = c;
    public void SetSize(float s) => brushSettings.brushSize = Mathf.RoundToInt(s);
    public void ClearCanvas() => paintingCanvas?.ClearCanvas();
    public void SaveCanvas() => paintingCanvas?.SaveCanvas();
}