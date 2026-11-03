using UnityEngine;

public class ParticlesOnCollision : MonoBehaviour
{
    public ParticleSystem particleSys;
    private void OnCollisionEnter(Collision collision)
    {
        // collision.gameObject returns the Rigidbody's object (Stage), not the wall itself
        if (collision.collider.CompareTag("Wall"))
        {
            ContactPoint contact = collision.GetContact(0);

            particleSys.transform.position = contact.point;
            particleSys.transform.rotation = Quaternion.LookRotation(contact.normal);
            particleSys.Play();
        }
    }
}
