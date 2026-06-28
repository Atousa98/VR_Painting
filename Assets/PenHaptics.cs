using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PenHaptics : MonoBehaviour
{
    
    public ActionBasedController rightController;

    [Range(0, 1)] public float intensity = 0.3f; 
    public float duration = 0.05f;  

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Draw Canvas"))
        {
            if (rightController != null)
            {
                rightController.SendHapticImpulse(intensity, duration);
                Debug.Log("Haptic Pulse Triggered!"); 
            }
        }
    }
}