using UnityEngine;

[CreateAssetMenu(fileName = "BrushSettings", menuName = "VRPainting/BrushSettings")]
public class BrushSettings : ScriptableObject
{
    [Range(1, 100)]
    public int brushSize = 12;

    [Range(0f, 1f)]
    public float hardness = 0.8f;

    public Color brushColor = Color.black;
}