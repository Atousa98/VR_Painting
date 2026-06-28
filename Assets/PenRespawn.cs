using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PenRespawn : MonoBehaviour
{
    [Header("Position Settings")]
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    [Header("Detection Settings")]
    [Tooltip("If the pen falls below this Y height, it will respawn")]
    public float deathYThreshold = 0.2f;

    private Rigidbody rb;
    private XRGrabInteractable grabScript;

    void Awake()
    {
        // Store the exact position on the table at the start of the game
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        grabScript = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        // Only respawn if the pen is NOT currently being held by the player
        if (grabScript != null && !grabScript.isSelected)
        {
            if (transform.position.y < deathYThreshold)
            {
                Respawn();
            }
        }
    }

    public void Respawn()
    {
        // Reset physics to stop it from "ghost falling" after teleporting
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Snap back to original table position
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        Debug.Log("Pen respawned to table.");
    }
}