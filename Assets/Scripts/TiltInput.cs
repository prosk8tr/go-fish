using UnityEngine;
using UnityEngine.InputSystem;

public class TiltInput : MonoBehaviour
{
    // 

    float sensitivity = 0.1f;
    float maxTilt = 25f; // degrees
    float smoothing = 10f;
    Vector2 targetTilt;
    public Vector2 tilt { get; private set; } // smoothed tilt, in degrees
    public Vector2 TiltStrength
    {
        get
        {
            return tilt / maxTilt; // applied to player when calculating movement force
        }
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null && mouse.leftButton.isPressed)
        {
            targetTilt += mouse.delta.ReadValue() * sensitivity;
            targetTilt = Vector2.ClampMagnitude(targetTilt, maxTilt);
        }
        else
        {
            targetTilt = Vector2.zero;
        }

        float lerpTime = 1f - Mathf.Exp(-smoothing * Time.deltaTime);
        tilt = Vector2.Lerp(tilt, targetTilt, lerpTime); 
    }
}
