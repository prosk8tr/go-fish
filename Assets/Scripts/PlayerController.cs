using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TiltInput input;
    float force = 20f; // max applicable force, in m/s
    Rigidbody rb;
    public CalculateYaw calcYaw;
    public Vector3 ForceDirection { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 tiltStrength = input.TiltStrength;
        Vector3 movement = new Vector3(tiltStrength.x, 0f, tiltStrength.y);
        Quaternion direction = Quaternion.Euler(0f, calcYaw.Yaw, 0f);
        ForceDirection = direction * movement;

        rb.AddForce(ForceDirection * force, ForceMode.Acceleration);
    }
}
