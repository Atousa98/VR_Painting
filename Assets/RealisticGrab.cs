using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class RealisticGrab : MonoBehaviour
{
    [Tooltip("Put XR Grab Interactable Pen in here")]
    public XRGrabInteractable grabInteractable;

    private InputAction buttonA = new InputAction("ButtonA", binding: "<XRController>{RightHand}/primaryButton");

    private void OnEnable()
    {
        buttonA.Enable();
    }

    private void OnDisable()
    {
        buttonA.Disable();
    }

    private void Update()
    {
        bool isPressed = buttonA.ReadValue<float>() > 0.1f;

        if (isPressed)
        {
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Default");
        }
        else
        {
            grabInteractable.interactionLayers = 0; 
        }
    }
}