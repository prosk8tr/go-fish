using UnityEngine;

public class ParticlesOnCollision : MonoBehaviour
{
    public ParticleSystem particleSys;
    public float minSpeed;
    Rigidbody ballRb;
    private void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") && ballRb.linearVelocity.magnitude > minSpeed)
        {
            ContactPoint contact = collision.GetContact(0);

            particleSys.transform.position = contact.point;
            particleSys.transform.rotation = Quaternion.LookRotation(contact.normal);
            particleSys.Play();
        }
    }
}
