using UnityEngine;

public class RotatePlayerModel : MonoBehaviour
{
    public Rigidbody rbBall;
    public float minSpeed = 2f;
    public float speedForMaxTurn = 8f;
    public float maxTurnSpeed = 300f;
    Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    // Returns the model to the rotation it had when the scene started
    public void ResetRotation()
    {
        transform.rotation = initialRotation;
    }

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
