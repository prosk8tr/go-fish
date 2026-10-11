using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public TiltInput input;
    void LateUpdate()
    {
        transform.position = ball.transform.position; // set camera pivot to ball position
        Vector2 cameraTilt = input.tilt;
        transform.rotation = Quaternion.Euler(-cameraTilt.y, 0f, cameraTilt.x);
    }
}
