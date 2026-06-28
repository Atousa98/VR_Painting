using UnityEngine;

public class VRPaintingCanvas : MonoBehaviour
{
    [Header("Canvas Settings")]
    public int textureWidth = 2048;
    public int textureHeight = 2048;
    public Color backgroundColor = Color.white;

    private Texture2D paintTexture;
    private Color32[] pixels;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        InitializeCanvas();
    }

    void InitializeCanvas()
    {
        paintTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        paintTexture.filterMode = FilterMode.Bilinear;
        paintTexture.wrapMode = TextureWrapMode.Clamp;

        pixels = new Color32[textureWidth * textureHeight];
        Color32 bgColor = backgroundColor;
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = bgColor;

        paintTexture.SetPixels32(pixels);
        paintTexture.Apply();

        Material mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        mat.mainTexture = paintTexture;
        mat.SetTexture("_BaseMap", paintTexture);
        meshRenderer.material = mat;
    }

    public void Paint(Vector2 uv, Color brushColor, int brushSize, float brushHardness)
    {
        int px = Mathf.RoundToInt(uv.x * textureWidth);
        int py = Mathf.RoundToInt(uv.y * textureHeight);
        DrawCircle(px, py, brushColor, brushSize, brushHardness);
        paintTexture.SetPixels32(pixels);
        paintTexture.Apply(false);
    }

    void DrawCircle(int centerX, int centerY, Color color, int radius, float hardness)
    {
        Color32 c = color;
        float radiusF = radius;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                float dist = Mathf.Sqrt(x * x + y * y);
                if (dist > radiusF) continue;

                int px = centerX + x;
                int py = centerY + y;
                if (px < 0 || px >= textureWidth || py < 0 || py >= textureHeight) continue;

                float alpha;
                float hardEdge = radiusF * hardness;
                if (dist <= hardEdge)
                    alpha = 1f;
                else
                    alpha = 1f - ((dist - hardEdge) / Mathf.Max(0.001f, radiusF - hardEdge));

                int index = py * textureWidth + px;
                pixels[index] = Color32.Lerp(pixels[index], c, alpha * color.a);
            }
        }
    }

    public void ClearCanvas()
    {
        Color32 bgColor = backgroundColor;
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = bgColor;
        paintTexture.SetPixels32(pixels);
        paintTexture.Apply();
    }

    public void SaveCanvas()
    {
        byte[] bytes = paintTexture.EncodeToPNG();
        string path = Application.persistentDataPath + "/painting.png";
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log("Saved to: " + path);
    }
}