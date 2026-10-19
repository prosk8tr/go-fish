using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public TiltInput input;
    public CalculateYaw calcYaw;
    public float pitch = 30f;

    void LateUpdate()
    {
        transform.position = ball.transform.position; // set camera pivot to ball position
        Vector2 cameraTilt = input.tilt;
        float yaw = calcYaw.Yaw;

        Quaternion yawRotation = Quaternion.Euler(0f, yaw, 0f);
        Quaternion tiltRotation = Quaternion.Euler(-cameraTilt.y, 0f, cameraTilt.x);
        Quaternion pitchRotation = Quaternion.Euler(pitch, 0f, 0f); // point camera down

        transform.rotation = yawRotation * tiltRotation * pitchRotation;
    }
}
