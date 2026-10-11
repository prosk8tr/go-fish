using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TiltInput input;
    float force = 20f; // max applicable force, in m/s
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 tiltStrength = input.TiltStrength;
        rb.AddForce(new Vector3(tiltStrength.x, 0f, tiltStrength.y) * force);
    }
}
