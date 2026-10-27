using UnityEngine;

public class RotatePlayerModel : MonoBehaviour
{
    public Rigidbody rbBall;
    public float minSpeed = 2f;
    public float speedForMaxTurn = 8f;
    public float maxTurnSpeed = 300f;
    void LateUpdate()
    {
        Vector3 velocity = rbBall.linearVelocity;
        velocity.y = 0f;
        float speed = velocity.magnitude;
        if (speed < minSpeed) return;

        Quaternion target = Quaternion.LookRotation(velocity);
        float t = Mathf.InverseLerp(minSpeed, speedForMaxTurn, speed);
        float turnSpeed = maxTurnSpeed * Mathf.Max(t, 0.1f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime);
    }
}
