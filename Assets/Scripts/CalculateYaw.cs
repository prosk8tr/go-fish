using UnityEngine;

public class CalculateYaw : MonoBehaviour
{
    public Rigidbody rbBall;
    float minSpeed = 0.2f; // Ball speed where turning starts
    float fullSpeed = 8f; // Ball speed for max turn rate
    float maxTurnRate = 120f; // Degrees per second
    float maxAngle = 120f; // For reversing, don't turn camera for backwards movement
    public float Yaw { get; private set; }
    float smoothTime = 0.4f;
    float yawVelocity;

    void LateUpdate()
    {
        // Get ball velocity and speed
        Vector3 velocity = rbBall.linearVelocity;
        velocity.y = 0f;
        float ballSpeed = velocity.magnitude;

        float targetYaw = Yaw;
        float maxRate = maxTurnRate;

        // If the ball moves fast enough, move the camera
        if (ballSpeed >= minSpeed)
        {
            // Get angle of the ball's movement
            float potentialTargetYaw = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
            // Get the shortest angle to stop the camera from spinning weirdly
            float delta = Mathf.DeltaAngle(Yaw, potentialTargetYaw);

            if (Mathf.Abs(delta) <= maxAngle)
            {
                targetYaw = potentialTargetYaw;
                float t = Mathf.InverseLerp(minSpeed, fullSpeed, ballSpeed);
                maxRate = maxTurnRate * Mathf.Max(t, 0.05f);
            }
        }
        Yaw = Mathf.SmoothDampAngle(Yaw, targetYaw, ref yawVelocity, smoothTime, maxRate);
    }
}
