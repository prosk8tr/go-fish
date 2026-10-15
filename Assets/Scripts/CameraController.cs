using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody ballrb;
    public TiltInput input;
    private float pitch = 30f;
    private float yaw;
    private void Start()
    {
        ballrb = ball.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        transform.position = ball.transform.position; // set camera pivot to ball position
        Vector2 cameraTilt = input.tilt;
        Vector3 direction = ballrb.linearVelocity.normalized;

        Quaternion tiltRotation = Quaternion.Euler(-cameraTilt.y, 0f, cameraTilt.x);
        Quaternion pitchRotation = Quaternion.Euler(pitch, 0f, 0f); // point camera down

        transform.rotation = yawRotation * tiltRotation * pitchRotation;
    }
}
